using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Data.Entity;
using System.Net;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CaseReview.WebApp.Models;

namespace IdentitySample.Models
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }

    public class EmailService : IIdentityMessageService
    {

        public async Task SendAsync(IdentityMessage message)
        {
            await SendAsync(message, null);
        }

        public async Task SendAsync(IdentityMessage message, List<Attachment> attachments)
        {
            // Plug in your email service here to send an email.

            string routeAllEmails = ConfigurationManager.AppSettings["routeAllEmails"];

            // Credentials:

            string smtpServer = ConfigurationManager.AppSettings["EmailSmtpServer"];
            //int smtpPort = int.Parse(ConfigurationManager.AppSettings["EmailSmtpPort"]);
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EmailEnableSSL"]);
            string smtpUsername = ConfigurationManager.AppSettings["EmailSmtpUsername"];
            string smtpPassword = ConfigurationManager.AppSettings["EmailSmtpPassword"];
            string sentFrom = ConfigurationManager.AppSettings["EmailSentFrom"];
            string port = ConfigurationManager.AppSettings["EmailPort"];

            // Configure the client:
            //var client = new SmtpClient(smtpServer, Convert.ToInt32(587));

            var client = new SmtpClient(smtpServer);

            if (!string.IsNullOrEmpty(port))
            {
                client.Port = int.Parse(port);
            }

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = enableSsl;
            client.Timeout = 200000;

            // Create the credentials:
            if (!string.IsNullOrEmpty(smtpPassword))
            {
                var credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.Credentials = credentials;
            }


            // Create the message:
            var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            if (routeAllEmails != string.Empty)
            {
                mail.To.Clear();
                mail.To.Add(new MailAddress(routeAllEmails));
            }
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    mail.Attachments.Add(attachment);
                }
            }
            //Utilities.LogUtil.Info("Sending email");

            // Send:
            await client.SendMailAsync(mail);

            //return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string twilioSid = ConfigurationManager.AppSettings["TwilioSid"];
            string twilioToken = ConfigurationManager.AppSettings["TwilioToken"];
            string twilioFromPhone = ConfigurationManager.AppSettings["twilioFromPhone"];
            //var Twilio = new TwilioRestClient(twilioSid, twilioToken);
            //var txtMessage = Twilio.SendMessage(twilioFromPhone, message.Destination, message.Body);

            // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            //Trace.WriteLine(result.Status);

            // Twilio doesn't currently have an async API, so return success.

            var task = Task.FromResult(0);

            return task;

        }

        private string AreYouGoing(bool answer)
        {
            return answer ? "Nice one" : "Mother f###er";
        }
    }

    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "System Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }


    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

}
