/* jshint esversion:6 */
/* jshint -W087 */
debugger;
var input = 303;
//var input = 3;
var counter = 1;
var position = 0;

var buffer = [0];
var bufferLen = 1;

var solution1 = 0;
var solution2 = 0;
for(var i = 0; i < 50000000; i++)
{
    if(i==2017)
    {
        solution1 = buffer[position+1];
    }
    position = (position + input) % bufferLen;
    if(i < 2017)
    {
        buffer.splice(position + 1, 0, counter);
    }
    bufferLen++;
    position++;
    if(position == 1)
    {
        solution2 = counter;
    }
    counter++;
}

console.log(solution1);
console.log(solution2);
