﻿namespace AOC_2023.Solutions;

public class Day02
{
    public int Part1(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var gameId = line.Split(":")[0];
            var id = gameId[5..];
            var draws = line.Split(": ")[1];
            var selections = draws.Split("; ");
            var invalid = false;
            foreach (var selection in selections)
            {
                var colours = selection.Split(", ");
                foreach (var colourAndQuantity in colours)
                {
                    var colour = colourAndQuantity.Split(" ")[1];
                    var quantity = int.Parse(colourAndQuantity.Split(" ")[0]);

                    if (colour == "red" && quantity > 12)
                        invalid = true;

                    if (colour == "green" && quantity > 13)
                        invalid = true;
                    
                    if (colour == "blue" && quantity > 14)
                        invalid = true;
                    
                }
            }

            if (!invalid)
                result += int.Parse(id);
        }

        return result;
    }

    public int Part2(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var gameId = line.Split(":")[0];
            var draws = line.Split(": ")[1];
            var selections = draws.Split("; ");

            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;
            
            foreach (var selection in selections)
            {
                var colours = selection.Split(", ");
                foreach (var colourAndQuantity in colours)
                {
                    var colour = colourAndQuantity.Split(" ")[1];
                    var quantity = int.Parse(colourAndQuantity.Split(" ")[0]);

                    if (colour == "red")
                        maxRed = Math.Max(maxRed, quantity);

                    if (colour == "green")
                        maxGreen = Math.Max(maxGreen, quantity);
                    
                    if (colour == "blue")
                        maxBlue = Math.Max(maxBlue, quantity);
                    
                }
            }

            result += (maxRed * maxGreen * maxBlue);
        }

        return result;
    }
}