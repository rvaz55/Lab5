// Declare a variable called 'changeColor' which will function as a shortcut to the "changeColor" element
//let changeColor = document.getElementById("changeColor");
let index = 0;
let changeColor = $("#changeColor");

//console.log(`wall found ? ${changeColor}`);

// Initialize button with user's preferred color
chrome.storage.sync.get("color", ({ color }) => {
  $("#changeColor").css("background-color", color);
  //changeColor.style.backgroundColor = color;
});

changeColor.on("click",async function(){
  
  //Create a variable called [tab] to hold a shortcut to the active-tab window
  let [tab] = await chrome.tabs.query({active: true, currentWindow: true});

  //Enable scripting on the [tab] object
  //Enable the setPageBackgroungColor function on the [tab] object
  chrome.scripting.executeScript({
    target: { tabId: tab.id },
    function: getSiteBlocker
  });
})

//Add an event listener to the changeColor button
//changeColor.addEventListener("click", async() => {
//  
//  //Create a variable called [tab] to hold a shortcut to the active-tab window
//  let [tab] = await chrome.tabs.query({active: true, currentWindow: true});
//
//  //Enable scripting on the [tab] object
//  //Enable the setPageBackgroungColor function on the [tab] object
//  chrome.scripting.executeScript({
//    target: { tabId: tab.id },
//    function: getSiteBlocker
//    //function: setPageBackgroundColor
//  });
//});

//Define the scripting function getSiteBlocker()
function getSiteBlocker(){
  console.log(`hits this line`)
  var wall = $("body").find("html")
  console.log(`wall found ? ${wall}`);
  console.log(`wall found ? ${wall}`);

   chrome.storage.sync.get("color", ({ color }) => {
    document.body.style.backgroundColor = color;
   })  

   console.log(`hits this line`)
  //element.parentElement.removeChild(element);

    //let index = 0;
    //let wall = document.getElementsByClassName("RnEpo");
//
    //let lengthOfWall = wall.length;
    //console.log(`length of wall ? ${lengthOfWall}`);
    ////let containsWall = wall.item(index);
    //if (lengthOfWall > 0)
    //{
    //  console.log(`does wall exist? ${lengthOfWall} !!`);
    //  wall.remove();
    //}
    //console.log(`length of wall ? ${lengthOfWall}`);
    //console.log(`length of wall ? ${lengthOfWall}`);
};

//Define the scripting function setPageBackgroungColor()
//function setPageBackgroundColor(){
//  //Access the 'color' obj which was stored in the sites local 'storage'
//   chrome.storage.sync.get("color", ({ color }) => {
//    document.body.style.backgroundColor = color;
//   })
//};