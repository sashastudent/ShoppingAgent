   M S B A M L   `   `   `  ���� Q  MTheLearningAgentClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TheLearningAgentClient.ViewModels!TheLearningAgentClient.ViewModels  -    'TheLearningAgentClient.Views.ItemInCart   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     F
ViewModels/clr-namespace:TheLearningAgentClient.ViewModels     -     >�Name$
  root��
�������.���� -   $��Scroller��$
��Auto=�.���� ����  �� Path$ MainScreen.ItemsInCart����� .R�]� $��ItemInPanel��
 � Tag �� $ 	ProductId� �� Mode$ OneWay=�.+��� $��5��$	��300��$��Bottom=���
� .���  � Color$ Black�� � Offset$ 0Q�� $ White��$ 1Q�� $ 	#FFF4F4F4��$ 0.896Q���  �� Orientation$ Vertical=�$��Right=�.+��� $��Right=�$��RightToLeft=�$ 
Horizontal=�.+�� $��Center=�$��Stretch=�$	��2 1q� � ColumnDefinitions �� $��1* � � RowDefinitions �� $��1* ��� $��1* �.+��� $��0��$��25��$��25������ 	 �� 
TargetType"	 ��.��� 
 �� Property
 � �� �� Value$A ;/TheLearningAgentClient;component/Photos/DeleteButtonBW.png�� �� Triggers ��  �� Binding �� *IsMouseOver+ �� ElementName$ btnDel�� �� Value True.a��� 
 � ��$? 9/TheLearningAgentClient;component/Photos/DeleteButton.png���� $��btnDel��$��0�� J� Opacity$ 0Q�$��25��$��25��$��D������ $ DelItem�$
 root������ $ ItemInPanel��$	 Tag��� $��1��$��25��$��25������ "	 ��.��� 
 � ��$? 9/TheLearningAgentClient;component/Photos/EditButtonBW.png�� ��  �� *IsMouseOver+$ btnEdit�� True.a��� 
 � ��$= 7/TheLearningAgentClient;component/Photos/EditButton.png���� $��btnEdit��$��1��$ 0Q�$��25��$��25��$��E������ *	ChangeQTY+$
 root������ $ ItemInPanel��$	 Tag��� ���	#FF727271$��16(�$��Center=�$��Center=�$��2q�$
��Bold"����� $ ProductName��� $��X�����	#FFF5CB85$��16(�$��Center=�$��Center=�$��1q��� ���	#FFF5CB85$��16(�$��Center=�$��Center=�$��2q����� $ Quantity����� "	 ��.��� 
 �@��ContentStringFormat$ {0:F0}�� ��  �� $ 	Weighable� True.a��� 
 �@��ContentStringFormat$ {0:F3}���� ���	#FFF5CB85$��12(�$��Center=�$��Center=�$��0 4 0 0q�$��1q����� "	 ��.��� 
 � ��$ יח'�� ��  �� $ 	Weighable� True.a��� 
 � ��$ קילו���� $��Center=���  �� ContentStringFormat$ {0:F2}�����	#FFA9A9A8$��12(�$��Right=�$��Center=�$��2q����� $ ProductPrice��� $ {0:F2}�����	#FFA9A9A8$��16(�$	��Top=�$
��Left=�$��4q����� $ Amount����� .R���  �� CanVerticallyScroll . 