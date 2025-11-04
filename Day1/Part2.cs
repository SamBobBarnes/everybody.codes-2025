namespace everybody.codes_2025.Day1;

public class Part2() : BasePart(1,2,@"Xanquor,Ylarcoryx,Lormal,Lirzeth,Arvnor,Braegnar,Lirdravor,Qyrafyr,Vyrvel,Drazgaz,Quarnkyr,Cynvarorath,Kroncyth,Zordar,Mornzrak,Iskargaz,Olarxar,Brylulrix,Adalynn,Vyraris

L13,R5,L12,R7,L8,R17,L18,R13,L9,R13,L5,R14,L5,R17,L5,R12,L5,R19,L5,R16,L5,R15,L12,R12,L15,R7,L17,R13,L18")
{
    public override string Run()
    {
        var input = Input();
        
        var names = input[0].Split(',');
        var instructions = input[2].Split(',').Select(x => x[0] == 'R' ? int.Parse(x[1..]) : -int.Parse(x[1..]));

        var currentIndex = 0;
        var length = names.Length;
 
        foreach (var instruction in instructions)
        { 
            currentIndex += instruction;
            if (currentIndex < 0) currentIndex += length;
            else if (currentIndex >= length) currentIndex -= length;
        }

        return names[currentIndex];
    }
}