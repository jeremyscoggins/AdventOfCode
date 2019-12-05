/* jshint esversion:6 */
/* jshint -W087 */
debugger;
// Generator A starts with 591
// Generator B starts with 393

var generatorA = 591;
var generatorB = 393;

//test
// var generatorA = 65;
// var generatorB = 8921;

var divisorA = 4;
var divisorB = 8;

var factorA = 16807;
var factorB = 48271;
var divisor = 2147483647;

var matches = 0;
for(var i = 0; i < 5000000; i++)
{
    do
    {
        generatorA = (generatorA * factorA) % divisor;
    } while (generatorA % divisorA != 0);
    //console.log(generatorA);

    do
    {
        generatorB = (generatorB * factorB) % divisor;
    } while (generatorB % divisorB != 0);
    //console.log(generatorB);

    // var binaryA = ("00000000000000000000000000000000" + generatorA.toString(2)).substr(-32);
    // console.log(binaryA);
    var lowerA = generatorA & 0xFFFF;

    // var binaryB = ("00000000000000000000000000000000" + generatorB.toString(2)).substr(-32);
    // console.log(binaryB);
    var lowerB = generatorB & 0xFFFF;

    if(lowerA == lowerB)
    //if(binaryA.substr(-16) == binaryB.substr(-16))
    {
        matches++;
    }
}


console.log(matches);