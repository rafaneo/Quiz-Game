using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
  

public class PanelMove : MonoBehaviour
{
    public Transform parent;
	public Transform answers_parent;
	public Transform timer;
	public Transform score;
	public bool timerOn;


	public int score_start = 0; 
	public float timeLeft;
	public float maxTime = 10f;
	public int index;
	public string correctAns;
	public int correctAnsIndex;
	public List<Questions> questions = new List<Questions>();
	public Questions currentQuestion; 
	
	[System.Obsolete]
	public void Start()
	{
		readTextFromFile();
		
		timerOn = true;
		timeLeft = maxTime;
		parent.GetChild(0).gameObject.SetActive(true);

		index = Random.Range(0, questions.Count);
		parent.GetChild(parent.childCount - 1).GetComponent<Text>().text = questions[index].questionText;
		for (int i = 0; i <answers_parent.childCount; i++)
		{

			answers_parent.GetChild(i).GetChild(0).GetComponent<Text>().text = questions[index].answers[i];
		}
		currentQuestion = questions[index]; 
		questions.RemoveAt(index);
	}

	public void evaluate_answer(GameObject my_game_object)
	{

		if (my_game_object.transform.GetSiblingIndex()+1 == currentQuestion.correctIndex)
			{
			Debug.Log(currentQuestion.correctIndex);
				my_game_object.GetComponent<Image>().color = Color.green;
			}
			else
			{
				my_game_object.GetComponent<Image>().color = Color.red;
				answers_parent.GetChild(currentQuestion.correctIndex-1).GetComponent<Image>().color = Color.green;
			}

		timerOn = false;
		StartCoroutine(Resetvalues());
	}
	public IEnumerator Resetvalues()
	{

		yield return new WaitForSeconds(1f);
		index = Random.Range(0, questions.Count);
		parent.GetChild(parent.childCount - 1).GetComponent<Text>().text = questions[index].questionText;
		for (int i = 0; i < answers_parent.childCount; i++)
		{
			answers_parent.GetChild(i).GetComponent<Image>().color = Color.white;
			answers_parent.GetChild(i).GetChild(0).GetComponent<Text>().text = questions[index].answers[i];
			
		}
		currentQuestion = questions[index];
		questions.RemoveAt(index);			
			timeLeft = maxTime;
			timerOn = true;
	}
	public void readTextFromFile()
	{
		string filetext = File.ReadAllText(@"C:\Users\rafai\Quiz Game\Assets\StreamReader\questions.txt");
		string[] lines = filetext.Split('\n');

		for (int i =0; i <lines.Length; i++)
		{
			string[] linecontent = lines[i].Split(',');
			Questions questionsinfo = new Questions(linecontent); 

			questions.Add(questionsinfo);
		}
		
	}
	public void  Resetvalues_Timer()
	{
		timeLeft = maxTime;
		index = Random.Range(0, questions.Count);
		parent.GetChild(parent.childCount - 1).GetComponent<Text>().text = questions[index].questionText;
		for (int i = 0; i < answers_parent.childCount; i++)
		{
			answers_parent.GetChild(i).GetChild(0).GetComponent<Text>().text = questions[index].answers[i];
			Debug.Log(questions[index].answers[i]);
		}
		currentQuestion = questions[index];
		questions.RemoveAt(index);
		for (int i = 0; i < answers_parent.childCount; i++)
		{
			answers_parent.GetChild(i).GetComponent<Image>().color = Color.white;
		}
	}
	public void Update()
	{
		if (timerOn == true)
		{
			timeLeft -= Time.deltaTime;
			timer.GetComponent<Image>().fillAmount = timeLeft / maxTime;
			if (timer.GetComponent<Image>().fillAmount == 0)
			{
				Resetvalues_Timer();

			}
		}
	}
	public void score_update()
	{
		//score.GetComponent<Text>.

	}
}



[System.Serializable]
public struct Questions
{

	public List<string> answers;
	public int correctIndex;
	public string questionText;

	public Questions(string[] content) : this()
	{
		answers = new List<string>();
		questionText = content[0];
		answers.Add(content[1]);
		answers.Add(content[2]);
		answers.Add(content[3]);
		answers.Add(content[4]);
		correctIndex = int.Parse(content[5]);
	}
}


