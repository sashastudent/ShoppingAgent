   M S B A M L   `   `   `  ���� Q  MTheLearningAgentClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TheLearningAgentClient.ViewModels!TheLearningAgentClient.ViewModels  (    "TheLearningAgentClient.Views.Login   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     F
ViewModels/clr-namespace:TheLearningAgentClient.ViewModels     $	��300��  N� FontSize$  14(� N� 
Background �	#FFBD9D9D N� Title$ Login�� � DataContext 6   0TheLearningAgentClient.ViewModels.LoginViewModel  .���� $��20q� �� CornerRadius$ 20�����#2e3137._��� $��20q�.+��� $��התחברות��
�������$��25(�$��Center=��� $��60q�$��
20 0 20 10q��� $��שם משתמש��
�������$��Center=��� -   $��txtUserName�����#545d6a
�������$��18(�����  �� Path$ Login.UserName� �� Mode$ TwoWay=��� $��ססמא��
�������$��Center=�G� -   $��txtPassword�����#545d6a
�������$��18(��� -   $��	btnSubmit��   BigFormBtnStyle#��[  $��שלח��$��60 10q����� $ ConnectCommand���  �� Orientation$ 
Horizontal=�$��Center=�.+��� -   $��btnExit��  SmallFormBtnStyle#��[ $
��צא������ $ ExitCommand��� -   $��btnRegistration��#��[ $��?לא רשום עדיין������ $ RegisterCommand�