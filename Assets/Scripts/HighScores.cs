using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public int[] scores = new int[10];
    string currentDirectory;
    public string scoreFileName = "highscores.txt";
    // Start is called before the first frame update
    void Start()
    {
        currentDirectory = Application.dataPath; //finds the directory using
        Debug.Log("Our current directory is: " + currentDirectory); //puts the directory into the debug log


        LoadScoresFromFile(); //loads the scores by default

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScoresFromFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName); //checks if the file is there
        if(fileExists == true)
        {
            Debug.Log("Found high score file " + scoreFileName); //will let you know if the file is found in the debug log
        }
        else
        {
            Debug.Log("The file " + scoreFileName + " does not exist. No scores will be loaded.", this); //will let you know if the file isnt there
            return;
        }

        scores = new int[scores.Length]; //will make a new array to make sure old values don't stick around

        StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFileName); //will read the directories

        int scoreCount = 0; //counter so it doesnt go past the end of the scores

        while (fileReader.Peek() != 0 && scoreCount < scores.Length) //runs while there is data to be read, and the array hasn't ended
        {
            string fileLine = fileReader.ReadLine();

            int readScore = -1;

            bool didParse = int.TryParse(fileLine, out readScore);

            if(didParse) //if its successfule put it in the array
            {
                scores[scoreCount] = readScore;
            }
            else //resets to the default value, if there is junk in the file
            {
                Debug.Log("Invalid line in scores file at " + scoreCount + ", using default value.", this);
                scores[scoreCount] = 0;
            }
            scoreCount++;

        }
        fileReader.Close(); //closes the stream
        Debug.Log("High scores read from " + scoreFileName);
    }
    public void SaveScoresToFile()
    {
        //creates a streamwriter
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);

        //writes to file
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }

        //close the stream
        fileWriter.Close();

        //log message
        Debug.Log("High scores written to " + scoreFileName);
    }
    public void AddScore(int newScore)
    {
        //adding the index it belongs at
        int desiredIndex = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            //uses break to stop the loop instead of checking the desiredIndex
            if (scores[i] > newScore || scores[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }


        if (desiredIndex < 0)
        {
            Debug.Log("Score of " + newScore + " not high enough for highscores list.", this);
            return;
        }// if no desired index found, score isn't high enough for table

        for (int i = scores.Length - 1; i > desiredIndex; i--)
        {
            scores[i] = scores[i - 1];
        }

        scores[desiredIndex] = newScore;
        Debug.Log("Score of " + newScore + " entered into high scores at position" + desiredIndex, this);



    }
}
