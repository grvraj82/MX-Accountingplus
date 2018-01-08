
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: editor.js
  Description: Javascript to form and support Rich text control
  Date Created : June 15, 07

 */


/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 13, 07         Rajshekhar D
*/


N = (document.all) ? 0 : 1;
var ob;
var over = false;
var pageX = 0
var pageY = 0
function MD(e) 
{
	if (over)
		{
			if (N) 
				{
					ob= document.getElementById("panel");
					X = e.layerX;
					Y = e.layerY;
					return false;
				}
			else 
				{
					ob = document.getElementById("panel");
					ob = ob.style;
					X=event.offsetX;
					Y=event.offsetY;
				}

		}
}

function MM(e) 
{
	if (ob) 
	{
		if (N) 
		{
			pageX = e.pageY-X;
			pageY =  e.pageY-Y;
			ob.style.top = e.pageY-Y;
			ob.style.left = e.pageX-X;
		}
		else 
		{
			ob.pixelLeft = event.clientX-X + document.body.scrollLeft;
			ob.pixelTop = event.clientY-Y + document.body.scrollTop;
			pageX = event.clientX-X + document.body.scrollLeft;
			pageY =  event.clientY-Y + document.body.scrollTop;
			return false;
		}
	}
}

function MU() 
{
	ob = null;
}

if (N) 
{
	document.captureEvents(Event.MOUSEDOWN | Event.MOUSEMOVE | Event.MOUSEUP);
}

document.onmousedown = MD;
document.onmousemove = MM;
document.onmouseup = MU;

function perform(task,para)
{
	switch(task)
	{
	
	case "Cut":
	        document.execCommand("Cut")
	        break; 
	case "Copy":
	        document.execCommand("Copy")
	        break; 
	case "Paste":
	        document.execCommand("Paste")
	        break;
	case "Undo":
	        document.execCommand("Undo")
	        break; 
        case "Redo":
	        document.execCommand("Redo")
	        break;  
        case "Save":
	        document.execCommand("SaveAs",true)
	        break; 
	case "Bold":
	        document.execCommand("Bold")
	        break;    
	case "Italic":
	        document.execCommand("Italic")
	        break;    
	case "UnderLine":
	        document.execCommand("Underline")
	        break;
	case "InsImage":
	        document.execCommand("InsertImage",true)
	        break;                 
	case "Anchor":
	        document.execCommand("CreateLink",true)
	        break;                 
	case "alignLeft":
            document.execCommand("JustifyLeft")              

	        break;                 
	case "alignCenter":
            document.execCommand("JustifyCenter")
            break;                 
 	case "alignRight":
        document.execCommand("JUSTIFYRIGHT")                  

	        break;
 	case "Justifyfull":
	        document.execCommand("JUSTIFYFULL")
	        break;
	case "Indent":
	        document.execCommand("Indent")
	        break;
	case "Outdent":
	        document.execCommand("Outdent")
	        break;  
	case "unorder":
	        document.execCommand("InsertUnorderedList",false,"disc")
	        break;                 
	case "order":
	        document.execCommand("InsertOrderedList",false,"I")
	        break;                 
	case "bgcolor":
	       // jv_ypos = parseInt(document.forms(0).posY.value)+35
	       // jv_xpos = document.forms(0).posX.value
		    colorPicker.style.visibility = "visible" 
		    //colorPicker.style.position = "absolute" 
	        colorPicker.style.left = pageX - 10 //jv_xpos
	        colorPicker.style.top = pageY - 10// jv_ypos
	        // var bgClr = document.design.Bgclr.value
	        //document.execCommand("BackColor",false,bgClr)
	        break;                 
	case "fontcolor":
	        colorPicker.style.visibility = "visible" 
            try
            {
                X = event.offsetX;
			    Y = event.offsetY;
			    pageX = event.clientX - X + document.body.scrollLeft;
			    pageY =  event.clientY - Y + document.body.scrollTop;
	            colorPicker.style.left = pageX + 20 
	            colorPicker.style.top = pageY + 25
	        }
	        catch(ex){}	        
	        break; 
	case "input":
	        para1 = "''"+para
	        document.execCommand("InsertInputText",false,para1)
	        break;
	case "fontsize":
	        var fntSize = parseInt(document.forms(0).fontSize.value)
	        document.execCommand("FontSize",true,fntSize)
	        break;     
	case "fontFmly":
	        var fntfmly = document.forms(0).fontfmly.value 
	        document.execCommand("FontName",true,fntfmly)
	        break;       
	}
        document.execCommand("STOP")
}

function SendColor(clr)
{
	var ColorFor = "FontColor"//document.forms(0).colorFor.value
	if(ColorFor=="FontColor")
	{
	// alert(clr)
	//sfClr.style.backgroundColor = clr
	//ftClr = document.design.Fntclr.value
	document.execCommand("ForeColor",true,clr)
	//var taskCase = "fontcolor"
	}
	else if(ColorFor=="bgColor")
	{
	document.execCommand("BackColor",false,clr)
	var taskCase = "bgcolor"
	} 
	colorPicker.style.visibility = "hidden"
}
