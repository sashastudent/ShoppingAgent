   M S B A M L   `   `   `  ����Q  MTheLearningAgentClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TheLearningAgentClient.ViewModels!TheLearningAgentClient.ViewModels  5      K*clr-namespace:TheLearningAgentClient.ViewsTheLearningAgentClient.Views  5      /    )TheLearningAgentClient.Views.Registration   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      F
ViewModels/clr-namespace:TheLearningAgentClient.ViewModels     5      <Views*clr-namespace:TheLearningAgentClient.Views     5        N� FontSize$  14(�5
       N� 
Background �	#FFBD9D9D N� Title$ Registration��5      $��RightToLeft=�5       � DataContext 5      =   7TheLearningAgentClient.ViewModels.RegistrationViewModel  5   
   .��5      �� $��20q�6    �� CornerRadius$ 20��6   ���#2e3137._�5   
   ]� .+�5      �� $	��Top=�6   $��
הרשמה��6)   
�������$��25(�6L   $��Center=�6Z   �� 5      $	��Top=�6   $��	20 0 20 0q�6-   �� 5      $��Bottom=�6    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��60 10q�6h   .+�5      �� -   5         ��Name$ btnOK��5      $��
אישור��6(   $	��2 2q�68   $	��2 2q�6F   ���#545d6a
�������$��18(�6{   ��6�   ��  �� Path$ 	OKCommand��� 5      -   5        $ 	btnCancel��5      $��
ביטול��6,   $	��2 2q�6<   $	��2 2q�6J   ���#545d6a
�������$��18(�6   ��6�   �� $ CancelCommand�5      �� 5      $��Bottom=�6   $
��Auto=�63   $��Disabled=�6X   $��20q�6   $	��261��6�   .��5       �� ��6   �� *Registration.LimitationsList+��5!      �� 5"      .R�5#      1   +TheLearningAgentClient.Views.EditLimitation  5$      ��5&      �� 5'      .R�5(      �� $ 
Horizontal=�6)   5)      5+      5,      �� 5-      $
��Left=�6   $
��20 0q�6/   .+�5.      �� $��שם פרטי��6   
�������$��Center=�6=   �� 5/      -   5         ��Name$ txtFirstName��5/   &   $	��120��6   ���#545d6a
�������$��18(�6e   ��6s   �� *Registration.FirstName+	 �� Mode$	 TwoWay=��� 50      $��שם משפחה��6   
�������$��Center=�6>   �� 51      -   5        $ txtLastName��51   &   $	��120��6   ���#545d6a
�������$��18(�6d   ��6r   �� *Registration.LastName+$	 TwoWay=��� 52      $��
כתובת��6   
�������$��Center=�6;   �� 53      -   5        $ 
txtAddress��53   &   $	��120��6   ���#545d6a
�������$��18(�6c   ��6q   �� *Registration.Address+$	 TwoWay=�54      �� 56      $��Right=�6   $
��20 0q�60   .+�57      �� $��שם משתמש��6   
�������$��Center=�6>   �� 58      -   5        $ txtUserName��58   &   $	��120��6   ���#545d6a
�������$��18(�6d   ��6r   �� *Registration.UserName+$	 TwoWay=��� 59      $��ססמא��6   
�������$��Center=�6:   G� 5:      -   5        
 G�Name$
 txtPassword��5:   *   $	��120��6   ���#545d6a
�������$��18(�6g   6u   �� 5;      $��אימות ססמא��6   
�������$��Center=�6@   G� 5<      -   5        $
 txtPassword2��5<   *   $	��120��6   ���#545d6a
�������$��18(�6h   6v   5=      5?      5@      5A      