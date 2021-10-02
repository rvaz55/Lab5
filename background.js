//This file contains logic to manipulate the sites DOM elements.

let color = '#3aa757';
let backgroundColor = '';

chrome.runtime.onInstalled.addListener(() =>{
    
    chrome.storage.sync.set({ color, backgroundColor });
    console.log('Default background color set to %cgreen', `color: ${color}`);
});
