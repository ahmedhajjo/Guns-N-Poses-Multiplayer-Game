using UnityEngine;


public struct ClickQuestions
{
    public string question;
    public int buttonID;
}
public class dataDictionary : MonoBehaviour
{
    public string randomResult = "";




    private static string[] wordList =
    {
        "Ahmed", "Teaching", "Learning", "suffering", "Surviving", "Diying", "judgment"

    };



    private static ClickQuestions[] clickQuestionList =
    {
       new ClickQuestions
       {
            question = "1 + 1 = 11",
            buttonID = 1},

         new ClickQuestions
         {
            question = "10 + 11 = 21",
            buttonID = 0},

         new ClickQuestions
         {
            question = "100 * 11 = 101",
            buttonID = 1},

         new ClickQuestions
       {
            question = "1 + 2 = 11",
            buttonID = 1},

         new ClickQuestions
         {
            question = "4 + 5 = 21",
            buttonID = 0},

         new ClickQuestions
         {
            question = "12 * 8 = 101",
            buttonID = 1},
    };

    public static string GetRandomWords()
    {

        int RandomIndex = Random.Range(0, wordList.Length);

        string randomWord = wordList[RandomIndex];
        return randomWord;
    }

    public static ClickQuestions GetClickQuestion()
    {
        return clickQuestionList[Random.Range(0, clickQuestionList.Length)];
    }

}
