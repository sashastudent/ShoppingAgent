   M S B A M L   `   `   `  ���� Q  MTheLearningAgentClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TheLearningAgentClient.ViewModels!TheLearningAgentClient.ViewModels  K*clr-namespace:TheLearningAgentClient.ViewsTheLearningAgentClient.Views  /    )TheLearningAgentClient.Views.Registration   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     F
ViewModels/clr-namespace:TheLearningAgentClient.ViewModels     <Views*clr-namespace:TheLearningAgentClient.Views       N� FontSize$  14(� N� 
Background �	#FFBD9D9D N� Title$ Registration��$��RightToLeft=� � DataContext =   7TheLearningAgentClient.ViewModels.RegistrationViewModel  .���� $��20q� �� CornerRadius$ 20�����#2e3137._�]� .+��� $	��Top=�$��
הרשמה��
�������$��25(�$��Center=��� $	��Top=�$��	20 0 20 0q��� $��Bottom=� �� Orientation$ 
Horizontal=�$��Center=�$��60 10q�.+��� -    ��Name$ btnOK��$��
אישור��$	��2 2q�$	��2 2q����#545d6a
�������$��18(�����  �� Path$ 	OKCommand��� -   $ 	btnCancel��$��
ביטול��$	��2 2q�$	��2 2q����#545d6a
�������$��18(����� $ CancelCommand��� $��Bottom=�$
��Auto=�$��Disabled=�$��20q�$	��261��.���� ���� *Registration.LimitationsList+���� .R�1   +TheLearningAgentClient.Views.EditLimitation  ���� .R��� $ 
Horizontal=��� $
��Left=�$
��20 0q�.+��� $��שם פרטי��
�������$��Center=��� -    ��Name$ txtFirstName��$	��120�����#545d6a
�������$��18(����� *Registration.FirstName+	 �� Mode$	 TwoWay=��� $��שם משפחה��
�������$��Center=��� -   $ txtLastName��$	��120�����#545d6a
�������$��18(����� *Registration.LastName+$	 TwoWay=��� $��
כתובת��
�������$��Center=��� -   $ 
txtAddress��$	��120�����#545d6a
�������$��18(����� *Registration.Address+$	 TwoWay=��� $��Right=�$
��20 0q�.+��� $��שם משתמש��
�������$��Center=��� -   $ txtUserName��$	��120�����#545d6a
�������$��18(����� *Registration.UserName+$	 TwoWay=��� $��ססמא��
�������$��Center=�G� -   
 G�Name$
 txtPassword��$	��120�����#545d6a
�������$��18(��� $��אימות ססמא��
�������$��Center=�G� -   $
 txtPassword2��$	��120�����#545d6a
�������$��18(�