/* jshint esversion:6 */
/* jshint -W087 */
debugger;
var input = `set i 31
set a 1
mul p 17
jgz p p
mul a 2
add i -1
jgz i -2
add a -1
set i 127
set p 316
mul p 8505
mod p a
mul p 129749
add p 12345
mod p a
set b p
mod b 10000
snd b
add i -1
jgz i -9
jgz a 3
rcv b
jgz b -1
set f 0
set i 126
rcv a
rcv b
set p a
mul p -1
add p b
jgz p 4
snd a
set a b
jgz 1 3
snd b
set f 1
add i -1
jgz i -11
snd a
jgz f -16
jgz a -19`;

// input = `set a 1
// add a 2
// mul a a
// mod a 5
// snd a
// set a 0
// rcv a
// jgz a -1
// set a 1
// jgz a -2`;

// input = `snd 1
// snd 2
// snd p
// rcv a
// rcv b
// rcv c
// rcv d`;


var instructions = input.split("\n");
var pattern = /(\w+) (\w+)( (\-?\w+))?/;
var registers = [{ P: 0 },{ p: 1 }];
var counter = [0,0];
var sound = [[],[]];
var programs = [0, 1];
var state = [0, 0];
var soundCount = [0,0];

while((state[0] == 0 && counter[0] < instructions.length) || (state[1] == 0 && counter[1] < instructions.length))
{
    var localSound = [null, null];
    for(var program of programs)
    {
        if(counter[program] >= instructions.length)
        {
            continue;
        }
        if(state[program] == 1)
        {
            //waiting
            if(sound[(program+1)%2].length == 0)
            {
                //nothing to receive
                continue;
            }
            else
            {
                state[program] = 0;
            }
        }
        var instruction = instructions[counter[program]];
        var match = pattern.exec(instruction);
        switch (match[1]) {
            case "snd":
                var value = getValue(program, match[2]);
                //sound[program].push(value);
                localSound[program] = value;
                soundCount[program]++;
                //console.log("play: " + sound);
    
                break;
        
            case "set":
                var register = match[2];
                var value = getValue(program, match[4]);
                setValue(program, register, value);
                
                break;
        
            case "add":
                var register = match[2];
                var value = getValue(program, match[4]);
                setValue(program, register, getValue(program, register) + value);
                
                break;
        
            case "mul":
            var register = match[2];
            var value = getValue(program, match[4]);
            setValue(program, register, getValue(program, register) * value);
                
                break;
        
            case "mod":
                var register = match[2];
                var value = getValue(program, match[4]);
                setValue(program, register, getValue(program, register) % value);
                
                break;
        
            case "rcv":
                var register = match[2];
                //if(getValue(program, register) > 0)
                {
                    if(sound[(program+1)%2].length > 0)
                    {
                        var value = sound[(program+1)%2].shift();
                        setValue(program, register, value);
                        //console.log("recover: " + sound);
                        //counter = instructions.length;

                    }
                    else
                    {
                        //wait
                        state[program] = 1;
                        continue;
                    }
                }
                
                break;
        
            case "jgz":
                var register = match[2];
                var value = getValue(program, match[4]);
                
                if(getValue(program, register) > 0)
                {
                    counter[program]+=value;
                    continue;
                }
    
                break;
        
            default:
                break;
        }
        counter[program]++;
    }
    if(localSound[0] != null)
    {
        sound[0].push(localSound[0]);
    }
    if(localSound[1] != null)
    {
        sound[1].push(localSound[1]);
    }
    //console.log(sound[1].length);
    //if(sound[1].length == 124)
    //{
    //    break;
    //}
}

console.log(soundCount[0]);
console.log(soundCount[1]);

function getValue(program, value)
{
    if(isNaN(value))
    {
        if(!registers[program].hasOwnProperty(value))
        {
            registers[program][value] = 0;
        }
        return registers[program][value];
    }
    return parseInt(value);
}

function setValue(program, register, value)
{
    registers[program][register] = value;
}