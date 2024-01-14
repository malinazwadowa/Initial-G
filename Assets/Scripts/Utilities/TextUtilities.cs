using UnityEngine;

public static class TextUtilities
{
    public static string FormatBigNumber(float number)
    {
        const float Billion = 1e9f;
        const float Million = 1e6f;
        const float Thousand = 1e3f;

        string formattedNumber;

        if (number >= Billion)
        {
            formattedNumber = $"{number / Billion:F1}B";
        }
        else if (number >= Million)
        {
            formattedNumber = $"{number / Million:F1}M";
        }
        else if (number >= Thousand)
        {
            formattedNumber = $"{number / Thousand:F1}k";
        }
        else
        {
            formattedNumber = $"{number:F1}";
        }


        if (formattedNumber.EndsWith(",0B") || formattedNumber.EndsWith(",0M") || formattedNumber.EndsWith(",0k") || formattedNumber.EndsWith(",0"))
        {
            if (formattedNumber.EndsWith(",0"))
            {
                formattedNumber = formattedNumber.Substring(0, formattedNumber.Length - 2);
            }
            else
            {
                formattedNumber = formattedNumber.Substring(0, formattedNumber.Length - 2) + formattedNumber[^1];
            }
        }

        return formattedNumber;
    }

    public static string FormatTime(float timeInSeconds)
    {
        int totalSeconds = Mathf.FloorToInt(timeInSeconds);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        int tensOfMinutes = minutes / 10;
        int wholeMinutes = minutes % 10;

        int tensOfSeconds = seconds / 10;
        int wholeSeconds = seconds % 10;

        string formattedTime = string.Format("{0}{1}:{2}{3}", tensOfMinutes, wholeMinutes, tensOfSeconds, wholeSeconds);

        return formattedTime;
    }
}