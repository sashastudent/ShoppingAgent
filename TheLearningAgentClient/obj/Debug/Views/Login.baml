   M S B A M L   `   `   `  ����Q  MTheLearningAgentClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TheLearningAgentClient.ViewModels!TheLearningAgentClient.ViewModels  5      (    "TheLearningAgentClient.Views.Login   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      F
ViewModels/clr-namespace:TheLearningAgentClient.ViewModels     5      $	��300��5        N� FontSize$  14(�5	       N� 
Background �	#FFBD9D9D N� Title$ Login��5
       � DataContext 5      6   0TheLearningAgentClient.ViewModels.LoginViewModel  5   
   .��5      �� $��20q�6    �� CornerRadius$ 20��6   ���#2e3137._�5   
   �� $��20q�6   .+�5      �� $��התחברות��6   
�������$��25(�69   $��Center=�6G   �� 5      $��60q�6   $��
20 0 20 10q�6%   6;   �� 5      $��שם משתמש��6   
�������$��Center=�6:   �� 5      -   5        $��txtUserName��5      ���#545d6a
�������$��18(�6S   ��6a   ��  �� Path$ Login.UserName� �� Mode$ TwoWay=��� 5      $��ססמא��6   
�������$��Center=�66   G� 5      -   5        $��txtPassword��5      ���#545d6a
�������$��18(�6U   6c   �� 5      -   5        $��	btnSubmit��5   >      BigFormBtnStyle#��[  6   $��שלח��6O   $��60 10q�6]   ��6m   �� $ ConnectCommand��� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5        $��btnExit��5   D     SmallFormBtnStyle#��[ 6   $
��צא��6S   ��6`   �� $ ExitCommand��� 5      -   5        $��btnRegistration��5   D   #��[ 6   $��?לא רשום עדיין��6[   ��6t   �� $ RegisterCommand�5       5!      5"      5#      