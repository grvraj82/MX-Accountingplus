function rbIsIE()
{
    if (navigator.appName == 'Microsoft Internet Explorer') {
        return true;
    }
    return false;
}

function rbIsOpera()
{
    if (navigator.appName == 'Opera') {
        return true;
    }
    return false;
}


function rbSupportsFixed()
{
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var agent = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        var version;
        if (re.exec(agent) != null) {
            version = parseFloat(RegExp.$1);
        }
        if (version < 5.0) {
            return false;
        }
        if (version < 7.0) {
            return false;
        }
    }
    return true;
}

var rbCenter = false;


function rbInit()
{

    if (rbSupportsFixed()) {
        div = document.getElementById('rbBackgroundDiv');
		
        if ( div!= null )
        {
            div.style.position ='fixed';
        }
		
    }	
    // I'd use onScroll, but that 
    // doesn't exist in standards mode
    setTimeout("rbReposition()", 50);
    rbResize();
}


var rbLastScrollTop = null;
var rbSimulateTop = 0;
 
function rbResize()
{	
    // We're in "standards mode," so we must use
    // document.documentElement, not document.body, in IE.
    var width;
    var height;
    var x, y, w, h;
    if (rbIsIE()) 
    {
        // All modern versions of IE, including 7, give the
        // usable page dimensions here.
        testsize = document.getElementById('rbTestSizeDiv');
        if ( testsize != null )
        {
            width = testsize.scrollWidth;
            height = testsize.scrollHeight;                                
            width = parseInt(document.body.scrollWidth)-1; 	
            height = parseInt(document.body.scrollHeight)-100; 	

        }


    } 
    else if (rbIsOpera()) 
    {
		
        width = parseInt(window.innerWidth) - 26;
		
        height = parseInt(window.innerHeight);
       

    } 
    else 
    {
		
        testsize = document.getElementById('rbTestSizeDiv');
        if ( testsize != null )
        {
            width = testsize.scrollWidth;
            height = testsize.scrollHeight;
        }
    }
    div = document.getElementById('rbBackgroundDiv');
    img = document.getElementById('rbBackground');
    if (rbCenter) {
        //alert("rbResize");
        if (img.width == 0) {
            // We don't know the width yet, the image
            // hasn't loaded. Set a timer to try again.
            alert("Don't know width yet");
            setTimeout("rbResize()", 1000);
            return;
        }
        w = width;
        h = width * (img.height / img.width);
        x = 0;
        y = (height - h) / 2;	
        if (y < 0) {
            h = height;
            w = height * (img.width / img.height);
            y = 0;
            x = (width - w) / 2;
        }
    } else {
        //alert("rbResizeelse");
        x = 0;
        y = 0;
        w = width;
        h = height;
    }
    // HTML 4.0 Strict makes the px suffix mandatory
    // We have floating point numbers, trim them and add px
    if ( div != null )
    {
        div.style.left = parseInt(x) + "px";
        if (rbSupportsFixed()) 
        {
            div.style.top = parseInt(y) + "px";
        } else {

            rbSimulateTop = parseInt(y);
        }
        img.style.width = parseInt(w) + "px";
        //img.style.height = parseInt(h) + "px";
        div.style.visibility = 'visible';
    }
    else
    {
    //img.style.width = parseInt(w) + "px";
    //img.style.height = parseInt(h) + "px";
    }
    rbLastScrollTop = null;
    rbReposition();
}

function rbReposition()
{
    if (rbSupportsFixed()) {
        return;
    }
    // Make sure we do this again
    setTimeout("rbReposition()", 50);
    // Standards mode, must use documentElement
    body = document.documentElement;
    var scrollTop = body.scrollTop;
    // No scroll since last check
    if (scrollTop == rbLastScrollTop) {
        return;
    }
    rbLastScrollTop = scrollTop;
    div = document.getElementById('rbBackgroundDiv');
    var rbBodyDiv = document.getElementById('rbBodyDiv');
    var pos = 0;
    // Don't make the user scroll just to see the background itself
    var max = rbBodyDiv.offsetHeight - rbBodyDiv.clientHeight;
    if (max < 0) {
        max = 0;
    }
    if (scrollTop <= max)
    {
        pos = scrollTop;
    } else {
        pos = max;
    }
    if (pos < 0) {
        pos = 0;
    }
    div.style.top = pos + rbSimulateTop;
}

function rbOpen(center)
{
    try {
        rbCenter = center;
        document.write("<div id='rbBodyDiv' style='position: absolute; z-index: 2'>\n");
    }
    catch (Error) {
    }

}

function rbClose(image)
{
    try {
        document.write("</div>\n");
        str = "<div " +
    "id='rbBackgroundDiv' " +
    "style='position: absolute; " +
    "  visibility: visible; " +
    "  top: 0px; " +
    "  left: 0px; " +
    "  z-index: -1'>" +
    "  <img src='" + image + "' id='rbBackground'>" +
    "</div>\n";
        //alert(str);
        document.write(str);
        document.write("<div " +
        "id='rbTestSizeDiv' " +
        "style='width: 100%; " +
        "  height: 100%; " +
        "  position: absolute; " +
        "  left: 0; " +
        "  top: 0; " +
        "  visibility: hidden; " +
        "  z-index: -1'></div>\n");
    }
    catch (Error) {
    }
}

