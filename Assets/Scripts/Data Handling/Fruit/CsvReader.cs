using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

public class CsvReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // Define delimiters, regular expression craziness
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r"; // Define line delimiters, regular experession craziness

    public static List<Fruit> Read(TextAsset data) //Declare method
    {
        var list = new List<Fruit>();

        // Split the data lines.
        var lines = Regex.Split(data.text.Trim(), LINE_SPLIT_RE);

        // Loops through the lines.
        for (var i = 0; i < lines.Length; ++i)
        {
            // Split the line.
            var values = Regex.Split(lines[i], SPLIT_RE);

            // Create the fruit.
            var spotSize = float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat);
            var spikeLength = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat);
            var poisonous = "0" != values[2];

            var fruit = new Fruit(spotSize, spikeLength, poisonous);

            list.Add(fruit);
        }

        return list;
    }
}