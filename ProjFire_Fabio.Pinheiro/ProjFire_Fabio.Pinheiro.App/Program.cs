
new StartUp();              // Initial config, setup all program
LogIn logIn = new LogIn();  // New login

bool showMenu = true;       // Run while users doesn't exit the app
while (showMenu)
{
    showMenu = logIn.Menu();
}
