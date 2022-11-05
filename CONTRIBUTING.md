# Contributing

## Setting up a development environment

### Installing Unity

Install Unity Editor version [2021.3.12f1 LTS](https://unity3d.com/unity/whats-new/2021.3.12). No other features are required.

### Installing a C# IDE

A C# IDE is recommended (if not required). Some options are:
- \[Recommended\] [JetBrains Rider](https://jetbrains.com/rider/)
    - Note that although JetBrains Rider costs money, it comes free with a student license (use your student email: xxxx@student.ubc.ca)
- [Visual Studio](https://visualstudio.microsoft.com)
    - Not as recommended since the Git support isn't the greatest, but the project also comes with the Github plugin, in case you use this.

It might be possible to use a simple text editor (e.g. [VS Code](https://vscode.dev)), but I highly recommend using a C# IDE.

### Installing Git

Install git from their [webpage](http://git-scm.com). I also highly recommend [GitHub Desktop](https://desktop.github.com/), but I don't use it since Rider has builtin Git support.

### Project Configuration

- Clone the repository using `git clone https://github.com/UBC-Game-Dev-Team3/GameDev2022T3`
  - I **Highly** recommend you do this via GitHub Desktop or Rider, since GitHub is known to have permissions issues if [2FA is enabled](https://docs.github.com/en/authentication/securing-your-account-with-two-factor-authentication-2fa/accessing-github-using-two-factor-authentication). This process has failed so many times for me - I've just used Rider since my GitHub credentials are saved there.
  - Note that you DO NOT have to create a project in Unity Hub - running git clone will download the entirety of the project (with the exception of some Git LFS shenanigans, but would be unlikely to encounter), but you would have to add it to Unity Hub via Open -> Add Project from Disk
- Follow these [steps in Unity](https://learn.unity.com/tutorial/set-your-default-script-editor-ide#) to integrate the IDE of choice with Unity
- Create new branches for features you are working on and do not commit directly to the master branch (unless you're making small changes, such as adding a png file)
- Use the master branch to pull the latest changes from the repo
  - If conflicts arise, Rider has some tools to help resolve simple conflicts
- Once all necessary changes have been committed **and pushed** to your own branch on GitHub, create a pull request on that branch
  - Although you can merge it yourself, we would advise doing so unless you know for a fact that it's not a breaking change, or if a deadline is approaching
