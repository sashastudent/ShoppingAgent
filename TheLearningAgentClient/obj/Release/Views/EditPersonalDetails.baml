   M S B A M L   `   `   `  ���� Q  MTheLearningAgentClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TheLearningAgentClient.ViewModels!TheLearningAgentClient.ViewModels  K*clr-namespace:TheLearningAgentClient.ViewsTheLearningAgentClient.Views  6    0TheLearningAgentClient.Views.EditPersonalDetails   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     F
ViewModels/clr-namespace:TheLearningAgentClient.ViewModels     <Views*clr-namespace:TheLearningAgentClient.Views       N� Title$  Registration�� N� FontSize$ 14(� N� 
Background �	#FFBD9D9D$��RightToLeft=� � DataContext D   >TheLearningAgentClient.ViewModels.EditPersonalDetailsViewModel  .���� $��20q� �� CornerRadius$ 20�����#2e3137._��� $��20q�.+��� $��עדכון פרטים��
�������$��25(�$��Center=��� $��60q�$��
20 0 20 20q���  �� Orientation$ 
Horizontal=�$��Center=�.+��� .+��� $��שם פרטי��
�������$��Center=��� -    ��Name$ txtFirstName��$	��120��$��10 0 5 0q����#545d6a
�������$��18(�����  �� Path$# EditPersonalDetails.FirstName� �� Mode$ TwoWay=��� .+��� $��שם משפחה��
�������$��Center=��� -   $ txtLastName��$	��120��$��5 0 10 0q����#545d6a
�������$��18(����� $" EditPersonalDetails.LastName�$ TwoWay=��� $ 
Horizontal=�$��Center=�.+��� .+��� $��
כתובת��
�������$��Center=��� -   $ 
txtAddress��$	��120��$��10 0 5 0q����#545d6a
�������$��18(����� $! EditPersonalDetails.Address�$ TwoWay=��� .+��� $��
טלפון��
�������$��Center=��� -   $ txtPhone��$	��120��$��5 0 10 0q����#545d6a
�������$��18(����� $ EditPersonalDetails.Phone�$ TwoWay=��� $
��Auto=�$��Disabled=�$��20q�$	��261��.���� ���� *%#EditPersonalDetails.LimitationsList+���� .R�1   +TheLearningAgentClient.Views.EditLimitation  ���� .R��� $ 
Horizontal=��� $��60q�$��20 20 20 20q��� $ 
Horizontal=�$��Center=�.+��� .+��� $��
סיסמא��
�������$��Center=�G� -   	 G�Name$	 txtPassword��$	��120��$��10 0 5 0q����#545d6a
�������$��18(��� .+��� $��אימות סיסמא��
�������$��Center=�G� -   $	 txtPassword2��$	��120��$��5 0 10 0q����#545d6a
�������$��18(��� $ 
Horizontal=�$��Center=�$��60 10q�.+��� -   
 ��Name$
 btnOK��   BigFormBtnStyle#��[  $��
אישור������ $ 	OKCommand��� -   $
 	btnCancel��#��[  $��
ביטול������ $ CancelCommand�