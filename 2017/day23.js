/* jshint esversion:6 */
/* jshint -W087 */
debugger;

// var input = `set b 67
// set c b
// jnz a 2
// jnz 1 5
// mul b 100
// sub b -100000
// set c b
// sub c -17000
// set f 1
// set d 2
// set e 2
// set g d
// mul g e
// sub g b
// jnz g 2
// set f 0
// sub e -1
// set g e
// sub g b
// jnz g -8
// sub d -1
// set g d
// sub g b
// jnz g -13
// jnz f 2
// sub h -1
// set g b
// sub g c
// jnz g 2
// jnz 1 3
// sub b -17
// jnz 1 -23`;


// var instructions = input.split("\n");
// var pattern = /(\w+) (\w+)( (\-?\w+))?/;
// var registers = {
//     a : 0,
//     b : 0,
//     c : 0,
//     d : 0,
//     e : 0,
//     f : 0,
//     g : 0,
//     h : 0
// };
// var counter = 0;
// var mulCount = 0;

// while(counter < instructions.length)
// {
//     var instruction = instructions[counter];
//     //console.log(counter, instruction);
//     var match = pattern.exec(instruction);
//     switch (match[1]) {
//         case "set":
//             var register = match[2];
//             var value = getValue(match[4]);
//             registers[register] = value;
//             break;
    
//         case "sub":
//             var register = match[2];
//             var value = getValue(match[4]);
//             registers[register] -= value;
//             break;
    
//         case "mul":
//             var register = match[2];
//             var value = getValue(match[4]);
//             registers[register] *= value;
//             mulCount++;
//             break;
    
//         case "jnz":
//             var value1 = getValue(match[2]);
//             if(value1 != 0)
//             {
//                 var value2 = getValue(match[4]);
//                 counter += value2;
//                 continue;
//             }
//             break;
    
//         default:
//             break;
//     }
//     counter++;
// }

// console.log(mulCount);

// function getValue( value)
// {
//     if(isNaN(value))
//     {
//         //if(!registers.hasOwnProperty(value))
//         //{
//         //    registers[value] = 0;
//         //}
//         return registers[value];
//     }
//     return parseInt(value);
// }

// function setValue(register, value)
// {
//     registers[register] = value;
// }


// var registers = {
//     a : 0,
//     b : 0,
//     c : 0,
//     d : 0,
//     e : 0,
//     f : 0,
//     g : 0,
//     h : 0
// };

// //registers.b = 67;
// //registers.c = registers.b;
// //if(a != 0)
// //{
// //    // skip next line
// //    routineA();
// //}
// //function routineA()
// //{
// //registers.b = 6700; //*= 100;
// registers.b = 106700; //-= -100000;
// //registers.c = 106700; //= registers.b;
// //registers.c = 123700; //-= -17000;
// //}
// registers.f = 1;
// registers.d = 2;
// //routineC();
// //function routineC()
// //{
// //    e = 2;
// //    routineB();
// //}
// //e = 2;
// //routineB();
// //function routineB()
// {
//     do
//     {
//         console.log(registers.d, registers.e, registers.f, registers.g, registers.h);
//         registers.e = 2;
//         do
//         {
//             console.log(registers.d, registers.e, registers.f, registers.g, registers.h);
//             registers.g = registers.d; //g = 2
//             registers.g *= registers.e; // 2 * 0
//             registers.g += 106700;//registers.b; 106700
//             if(registers.g == 0)
//             {
//                 registers.f = 0;
//             }
//             registers.e += 1; //1
//             registers.g =  registers.e; //1
//             registers.g += 106700;//registers.b; //106701
//         } while(registers.g != 0);
//         //if(g != 0)
//         //{
//         //    routineB();
//         //}
//         registers.d += 1;
//         registers.g = registers.d;
//         registers.g += 106700;//registers.b;
//     } while(registers.g != 0);
//     //if(g != 0)
//     //{
//     //    //routineC();
//     //    e = 2;
//     //    routineB();
//     //}
//     if(registers.f == 0)
//     {
//         registers.h += 1;
//     }
//     registers.g = 106700;//registers.b;
//     registers.g += 123700;//registers.c;
//     //jnz 1 3
//     //sub b -17
//     //jnz 1 -23
// }

// console.log(registers.d, registers.e, registers.f, registers.g, registers.h);

let nonPrimes = 0;
for(let n = 106700; n <= 123700; n += 17)
{
    let d = 2;
    while(n % d !== 0)
    {
        d++;
    }
    if(n !== d)
    {
        nonPrimes++;
    }
}

console.log(nonPrimes);