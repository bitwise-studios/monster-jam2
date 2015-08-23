using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestQuestionsBehaviour : MonoBehaviour {

	public static string[][] hardQuestions = {
		new string[]
		{"According to “Controlling 64 Bit Architectures and Write-Ahead Logging with EEL,” why must XML work?",
			"Symmetric encryption can be applied to the visualization of B-trees",
			"Asymmetric encryption can be used to balance BFS-trees",
			"Write ahead logging is used in the production of multi-level page tables, which allows for efficient creation of a Java tree.",
			"Hackers use superpages to reduce the signal-to-noise ratio of EEL, which is a function of signal-to-noise ratio.",
			"0"
		},
		new string[]
		{"According to Nietzsche, what doe a blah need to do to become an Ubermensch?",
			"Choose master morality over slave morality by setting the jumper on your IDE drive in order to exchange truths for illusions",
			"find the spell of reality that creates this vulgar and vast perspective and run Microsoft Word's spell checker upon it",
			"love your enemies and hate your friends in equal measure where the measure, k, is a function of partial pressures",
			"Remove causality, correctness, and expression between spheres until only aesthetic relations remain",
			""
		},
		new string[]
		{"Given that mumble mumble is mumble, what is the radius of the earth?",
			"10000km",
			"6,731,000,000,000mm",
			"4096m",
			"MINECRAFT",
			""
		},
		new string[]
		{"Given that the blah blah is blah blah, what is the blah blah blah of 5?",
			"Blah",
			"Blah blah",
			"Mumble",
			"I DON'T EVEN",
			""
		}
	};

	private string[][] easyQuestions = {
		new string[]
		{"1+1=?",
			"3",
			"2",
			"sin(0.5)",
			"11",
			"1"
		},
		new string[]
		{"4*5=?",
			"9.3",
			"2x",
			"77",
			"20",
			"3"
		},
		new string[]
		{"What is the capital of the USA?",
			"Washington DC",
			"New York",
			"Chicago",
			"Whiterun",
			"0"
		},
		new string[]
		{"What is the capital of Canada?",
			"Toronto",
			"Halifax",
			"Ottawa",
			"Swagville",
			"2"
		}
	};
	private string[][] questions;
	private int questionIndex;
	public Text[] buttonTexts = new Text[4];
	public Text questionText;
	private string correctResponse;
	public Text correctText;
	private int score = 0;

	// Use this for initialization
	void Start () {
		questions = easyQuestions;//hardQuestions;
		questionIndex = -1;
		nextQuestion();
	}

	void nextQuestion() {
		questionIndex++;
		if (questionIndex >= questions.Length) {
			// done
			for (int i = 0; i < 4; i++) {
				buttonTexts[i].gameObject.transform.parent.gameObject.SetActive(false);
			}
            GlobalState.Instance.TestResult = score / questions.Length;
			questionText.text = "You correctly answered " + score + " of " + questions.Length + " questions.";
			return;
		}
		string[] questionData = questions[questionIndex];
		for (int i = 0; i < 4; i++) {
			buttonTexts[i].text = questionData[1+i];
		}
		questionText.text = questionData[0];
		correctResponse = questionData[5];
	}

	public void QuizButtonClicked(string s) {
		bool carrot = s.Equals(correctResponse);
		//correctText.text = (carrot? "Correct!": "WRONG");
		score += (carrot? 1: 0);
		nextQuestion ();
	}
}
