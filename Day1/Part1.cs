namespace everybody.codes_2025.Day1;

public class Part1() : BasePart(1,1)
{
    public override string Run()
    {
        var input = Input();
        
        var names = input[0].Split(',');
        var instructions = input[2].Split(',').Select(x => (Direction: x[0], Steps: int.Parse(x[1].ToString())));

        var currentIndex = 0;
        var length = names.Length;
 
        foreach (var instruction in instructions)
        {
            if(instruction.Direction == 'R')
            {
                currentIndex += instruction.Steps;
                currentIndex %= length;
            }
            else
            {
                currentIndex -= instruction.Steps;
                if (currentIndex < 0)
                    currentIndex = 0;
            }
        }

        return names[currentIndex];
    }
}