mergeInto(LibraryManager.library, {
    //add js functions here and call them from C# code

    EmitJSEvent: function (eventName, arg1, arg2, arg3) {
        if(window.unityJSEmitter) {
            window.unityJSEmitter(
                UTF8ToString(eventName), 
                UTF8ToString(arg1 || ""), 
                UTF8ToString(arg2 || ""), 
                UTF8ToString(arg3 || "")
            );
        }
    },
  });