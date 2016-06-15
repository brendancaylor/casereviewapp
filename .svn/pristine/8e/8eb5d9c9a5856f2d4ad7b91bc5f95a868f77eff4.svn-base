$(function () {

    ko.dirtyFlag = function (root, isInitiallyDirty) {
        var result = function () { },
            _initialState = ko.observable(ko.toJSON(root)),
            _isInitiallyDirty = ko.observable(isInitiallyDirty);

        result.isDirty = ko.computed(function () {
            return _isInitiallyDirty() || _initialState() !== ko.toJSON(root);
        });

        result.reset = function () {
            _initialState(ko.toJSON(root));
            _isInitiallyDirty(false);
        };

        return result;
    };

    ko.extenders.confirmable = function (target, options) {
        var message = options.message;
        //var unless = options.unless || function () { return false; }
        var callback = options.callback || function () { return false; }

        //create a writeable computed observable to intercept writes to our observable
        var result = ko.computed({
            read: target,  //always return the original observables value
            write: function (newValue) {
                var current = target();

                //ask for confirmation unless you don't have
                if (confirm(message)) {
                    target(newValue);
                    if (callback) {
                        callback();
                    }
                }
                else {
                    if (newValue !== current) {
                        target.notifySubscribers(current);
                    }
                }
            }
        }).extend({ notify: 'always' });

        //return the new computed observable
        return result;
    };


    app.viewmodel = (function () {
        var self = {};
        self.isBound = ko.observable(false);
        return self;
    })();

});

