using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class Usuario
{
    public string colegio;
    public string grado;
    public string email;
    public string firstname;
    public string lastname;
    public string username;

    //public string testest;

    //public Rol roll;

    public Usuario()
    {
    }

    public Usuario(string colegio, string grado, string email, string firstname, string lastname, string username)
    {
        this.colegio = colegio;
        this.grado = grado;
        this.email = email;
        this.firstname = firstname;
        this.lastname = lastname;
        this.username = username;
        //this.roll = new Rol(1, "Estudiante");
    }
}


public class Rol
{
    public int id;
    public string value;

    public Rol()
    {
        this.id = 1;
        this.value = "Estudiante";
    }


    public Rol(int id, string value)
    {
        this.id = id;
        this.value = value;
    }

}

public class AuthManager : MonoBehaviour
{
    AuthUI authUI;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;



    bool flag = true;
    bool flagBackCreate = false;
    bool flagSingnOut = false;
    bool flagLogin = false;
    // Start is called before the first frame update

    //DATABASE

    DatabaseReference reference;
    Usuario userBD;
    Rol rol;
    //END DATABASE
    void Start()
    {

        //  DATABASE  https://engame-test.firebaseio.com/
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://engame-test.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //  END DATABASE
        authUI = GetComponent<AuthUI>();
        //auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        InitializeFirebase();
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }

    public void OnCreateBackButton()
    {
        flagBackCreate = true;
    }



    public void OnLoginButtonClick()
    {
        Debug.Log("apretaron botton login");
        TryLoginWithFirebaseAuth(authUI.loginEmail.text, authUI.loginPassword.text);
        //authUI.ShowLoggedinPanel();//ejemplo
        //authUI.loggedinUserEmail.text = newUser.Email;
    }

    public void OnCreateaccountButtonClick()
    {
        Debug.Log("presionaron create");
        //userBD = new Usuario(authUI.nombres.text, authUI.apellidos.text, authUI.signupEmail.text);
        //userBD.nombre = authUI.nombres.text;
        //userBD.apellido = authUI.apellidos.text;
        //userBD.email = authUI.signupEmail.text;
        //string pass = authUI.signupPassword.text;
        //string confpass = authUI.signupConfirmPassword.text;
        TrySignupWithFirebaseAuth(authUI.colegio.text, authUI.grado.text, authUI.nombres.text, authUI.apellidos.text , authUI.username.text, authUI.signupEmail.text, authUI.signupPassword.text, authUI.signupConfirmPassword.text);
        //TrySignupWithFirebaseAuth(nombr, ape, ema, pass, confpass);
        Debug.Log("sali de la funcionn");
        //authUI.ShowLoginPanel();
        Debug.Log("sali de la funcionnyyyeee");
    }

    public void OnSignoutButtonClick()
    {
        auth.SignOut();
        flagSingnOut = true;
        //authUI.ShowLoginPanel();
    }

    public void OnSigninGoogleClick()
    {

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp, i.e.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                // where app is a Firebase.FirebaseApp property of your application class.

                // Set a flag here indicating that Firebase is ready to use by your
                // application.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void TryLoginWithFirebaseAuth(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            //authUI.ShowLoggedinPanel();
            authUI.loggedinUserEmail.text = newUser.Email;
            flagLogin = true;
        });
    }

    public string GetStringID(string email)
    {
        string salida = email.Substring(0,email.Length - 4);


        //sad@gmail.com

        return salida;
    }

    private void TrySignupWithFirebaseAuth(string colegio, string grado, string firstname, string lastname, string username,  string email, string password, string confirmPassword)
    {
        if (password != confirmPassword) { 
            //Create Alert
            Debug.Log("las contraseñas no coinciden"); 
        
            return; 

            }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            Debug.Log("COMPOETO ");




            if (task.IsCompleted && flag)
            {
                //Alert: Registro Completo

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                //authUI.ShowLoginPanel();


                string ema = GetStringID(email);
                //rol , Usuario
                userBD = new Usuario(colegio, grado, email, firstname, lastname, username);
                string json = JsonUtility.ToJson(userBD);
                //string json01 = JsonUtility.ToJson(rol);
                reference.Child("usersF").Child(newUser.UserId).SetRawJsonValueAsync(json);

                //reference.Child("usersF").Child(newUser.UserId).Child("rol").Child("0").SetRawJsonValueAsync(json01);

                flag = false;


                //DATABASE


                // END DATABASE



            }
            // Firebase user has been created.



        });
        //Debug.Log(flag);



    }

    /*
   private void TrySignupWithGoogleAuth()
    {



        Firebase.Auth.Credential credential =
        Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (!flag)
        {
            flag = true;
            authUI.ShowLoginPanel();//

        }
        if (flagBackCreate)
        {
            flagBackCreate = false;
            authUI.ShowLoginPanel();
        }
        if (flagSingnOut)
        {
            flagSingnOut = false;
            authUI.ShowLoginPanel();
        }
        if (flagLogin)
        {
            flagLogin = false;
            authUI.ShowLoggedinPanel();
        }
    }
}
