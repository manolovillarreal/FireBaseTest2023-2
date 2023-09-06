using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSignup : MonoBehaviour
{

    [SerializeField]
    private Button _registrationButton;
    private Coroutine _registrationCoroutine;

    void Reset()
    {
        _registrationButton = GetComponent<Button>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        _registrationButton.onClick.AddListener(HandleRegisterButtonClicked);
    }

    private void HandleRegisterButtonClicked()
    {
        string email = GameObject.Find("InputEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputPassword").GetComponent<TMP_InputField>().text;

        _registrationCoroutine =  StartCoroutine(RegisterUser(email, password));

    }

    private IEnumerator RegisterUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var  registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.IsCanceled)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");

        }
        else if (registerTask.IsFaulted)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + registerTask.Exception);
        }
        else
        {
            // Firebase user has been created.
            Firebase.Auth.AuthResult result = registerTask.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        }       

    }
}
