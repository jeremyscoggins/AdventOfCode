/* jshint esversion:6 */
/* jshint -W087 */
debugger;
var input = [0,5,10,0,11,14,13,4,11,8,8,7,1,4,12,11];
var states = new Map();
var lastSeen = input.join() 

while(!states.has(lastSeen))
{
    states.set(lastSeen, states.size);
    //console.log(input.join("\t"));

    var max = Math.max.apply(null, input);
    var maxBank = input.indexOf(max);
    
    //console.log("max bank: " + maxBank);

    input[maxBank] = 0;
    var start = maxBank;
    while(max > 0)
    {
        start = (start + 1) % input.length;
        input[start]++;
        max--;
    }
    lastSeen = input.join();
}
//console.log(input.join("\t"));
console.log(states.size);
console.log(states.size - states.get(lastSeen));