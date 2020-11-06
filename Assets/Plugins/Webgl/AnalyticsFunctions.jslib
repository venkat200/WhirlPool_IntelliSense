

mergeInto(LibraryManager.library, 
{
 
    intialize :  function()
    {
        var  scr = document.createElement("script");
        scr.src="https://www.google-analytics.com/analytics.js";
        scr.addEventListener('load', function() {
            window.ga=window.ga||function(){(ga.q=ga.q||[]).push(arguments)};ga.l=+new Date;
            ga('create', 'UA-179657289-1', 'auto');
            ga('send', 'pageview');
        });
        document.body.appendChild(scr);
    },

    LogEvent :  function(str)
    {
		console.log(ga.q);
	
		var eventname = Pointer_stringify(str);
		
		console.log(eventname);
		
        window.ga=window.ga||function(){(ga.q=ga.q||[]).push(arguments)};ga.l=+new Date;
        ga('send', 'event',eventname,'clicked',eventname);
    }

});