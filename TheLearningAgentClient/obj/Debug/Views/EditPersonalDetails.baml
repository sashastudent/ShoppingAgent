   M S B A M L   `   `   `  ����Q  MTheLearningAgentClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TheLearningAgentClient.ViewModels!TheLearningAgentClient.ViewModels  5      K*clr-namespace:TheLearningAgentClient.ViewsTheLearningAgentClient.Views  5      6    0TheLearningAgentClient.Views.EditPersonalDetails   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      F
ViewModels/clr-namespace:TheLearningAgentClient.ViewModels     5      <Views*clr-namespace:TheLearningAgentClient.Views     5        N� Title$  Registration��5	       N� FontSize$ 14(�5
       N� 
Background �	#FFBD9D9D$��RightToLeft=�5       � DataContext 5      D   >TheLearningAgentClient.ViewModels.EditPersonalDetailsViewModel  5   
   .��5      �� $��20q�6    �� CornerRadius$ 20��6   ���#2e3137._�5   
   �� $��20q�6   .+�5      �� $��עדכון פרטים��6   
�������$��25(�6=   $��Center=�6K   �� 5      $��60q�6   $��
20 0 20 20q�6%   �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� .+�5      �� $��שם פרטי��6   
�������$��Center=�6A   �� 5      -   5         ��Name$ txtFirstName��5   =   $	��120��6   $��10 0 5 0q�6*   ���#545d6a
�������$��18(�6|   ��6�   ��  �� Path$# EditPersonalDetails.FirstName� �� Mode$ TwoWay=�5      �� 5      .+�5      �� $��שם משפחה��6   
�������$��Center=�6B   �� 5      -   5        $ txtLastName��5   =   $	��120��6   $��5 0 10 0q�6*   ���#545d6a
�������$��18(�6{   ��6�   �� $" EditPersonalDetails.LastName�$ TwoWay=�5      5      �� 5       $ 
Horizontal=�6   $��Center=�62   .+�5!      �� .+�5"      �� $��
כתובת��6   
�������$��Center=�6?   �� 5#      -   5        $ 
txtAddress��5#   <   $	��120��6   $��10 0 5 0q�6*   ���#545d6a
�������$��18(�6y   ��6�   �� $! EditPersonalDetails.Address�$ TwoWay=�5$      �� 5%      .+�5&      �� $��
טלפון��6   
�������$��Center=�6?   �� 5'      -   5        $ txtPhone��5'   <   $	��120��6   $��5 0 10 0q�6*   ���#545d6a
�������$��18(�6w   ��6�   �� $ EditPersonalDetails.Phone�$ TwoWay=�5(      5)      �� 5+      $
��Auto=�6   $��Disabled=�6@   $��20q�6g   $	��261��6t   .��5,      �� ��6   �� *%#EditPersonalDetails.LimitationsList+��5-      �� 5.      .R�5/      1   +TheLearningAgentClient.Views.EditLimitation  50      ��52      �� 53      .R�54      �� $ 
Horizontal=�6)   55      57      58      �� 5:      $��60q�6   $��20 20 20 20q�6%   �� 5<      $ 
Horizontal=�6   $��Center=�62   .+�5=      �� .+�5>      �� $��ססמא��6   
�������$��Center=�6>   G� 5?      -   5        	 G�Name$	 txtPassword��5?   @   $	��120��6"   $��10 0 5 0q�6.   ���#545d6a
�������$��18(�6}   6�   5@      �� 5A      .+�5B      �� $��אימות ססמא��6   
�������$��Center=�6D   G� 5C      -   5        $	 txtPassword2��5C   @   $	��120��6"   $��5 0 10 0q�6.   ���#545d6a
�������$��18(�6~   6�   5D      5E      �� 5G      $ 
Horizontal=�6   $��Center=�62   $��60 10q�6O   .+�5H      �� -   5        
 ��Name$
 btnOK��5H   B      BigFormBtnStyle#��[  6   $��
אישור��6Q   ��6a   �� $ 	OKCommand��� 5I      -   5        $
 	btnCancel��5I   B   #��[  6   $��
ביטול��6U   ��6e   �� $ CancelCommand�5J      5K      5L      5M      