   M S B A M L   `   `   `  ����=  9PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LPLP  5   	       PL.NewOrderWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:LP     5   	     � Title$  NewOrderWindow��5   	   $	��640��6    $	��800��6-   .��5	      � -   5         �Name$ GridMain��5	       � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��1* �6   .+�5   
   � -   5        $��GridUp��5      $��Stretch=�6   $
��auto��6;   $��0��6I   $��Stretch=�5      $
��auto��6-    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ SelectorProduct��5      $��1��6\   $��133,52,205,35q�6l    J� RenderTransformOrigin$ 
0.915,0.38&�6�   $��2��6�   ��6�   Z� �� 5      -   5         ��Name$ LabelProduct��5      $��1��6*   $��Choose a product 🔎��6:   $
��Left=�6X   $��0,58,0,0q�6s   $	��Top=�6�   $	��128��6�   ���	#FFEC79896�   �� 5      -   5         ��Name$ FilterDelete��5      $��2��6+   $	��❎��6;   $
��Left=�6b   $��	77,60,0,0q�6}   $	��Top=�6�   $��22��6�   $��28��6�   
���������6�   Z� 5      �� 5   
   -   5         ��Name$ NewOrderView��5      $��1��6)   $��18(�6a   	 �� View	 5      �� 5      
 �� AllowsColumnReorder$

 true��6   .P�5      �� -   5        NameProduct��$
��Name��5   e    �� Width$	 420��6s    �� DisplayMemberBinding 6:   ��  �� Path$
 Name�6s   �� 5      -   5        PriceProduct��$��Price��5   g   $ 80��6v    6;   �� $ Price�6v   �� 5      $ 30��6%    �� CellTemplate 5       �� 5!      .R�5"   "   �� $
��💲��6D   $��40��6Q   
 � Tag 6(   �� *NameProduct+6Q   5#      5%      5&      5(      �� 5)   
   -	   5        $��Go To Cart 🛒��5)      $��	618,0,0,0q�6)   $��2��6<      
classicBtn#��[  6I   $��Center=�6�   $	��144��6�   $��40��6�   $ 0.278,0.535&�6�   �� 5*   
   $
��Left=�6   $��	0,374,0,0q�6/   $��1��6B   $>��8To see the product details, double click on the product.��6O   $	��Top=�6�   $	��494��6�   $��2��6�   $��35��6�   $
��Wrap=�6�    �� TextAlignment$ Center=�6�   $��18(�6�   �� 5+   
   -
   5        $��
Go Back↺��5+      #��[  6$   $��38,0,0,0q�6]   $	��105��6o   $��40��6{   $��2��6�   $��Center=�6�   5,      5-      