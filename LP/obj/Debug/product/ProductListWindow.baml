   M S B A M L   `   `   `  ����=  9PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LPLP  5   	       PL.ProductListWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:LP     5   	     � Title$  ProductListWindow��5   	   $	��640��6#   $	��800��60   .��5	      � -   5         �Name$ MainGrid��5	       � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��1* �6   .+�5   
   � -   5        $��UpGrid��5      $��Stretch=�6   $
��auto��6;   $��0��6I   $��Stretch=�5      $
��auto��6-    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ ProductSelector��5      $��1��6\   $��133,52,205,35q�6l    J� RenderTransformOrigin$ 
0.915,0.38&�6�   $��2��6�   ��6�   Z� �� 5      -   5         ��Name$ ProductLabel��5      $��1��6*   $��Choose a product��6:   $
��Left=�6U   $��	10,58,0,0q�6p   $	��Top=�6�   $	��101��6�   ���	#FFEC79896�   �� 5      -   5         ��Name$ DeleteFilter��5      $��2��6+   $��X��6;   $
��Left=�6b   $��	94,52,0,0q�6}   $	��Top=�6�   $��38��6�   $��40��6�   
���   ���6�   Z� 5      �� 5   
   -   5         ��Name$ ProductListView��5      $��1��6,   $��18(�6�   	 �� View	 5      �� 5      
 �� AllowsColumnReorder$

 true��6   .P�5      �� -   5        	ProductId��$��ID��5   a    �� Width$	 120��6m    �� DisplayMemberBinding 68   ��  �� Path$ ID�6m   �� 5      -   5        ProductName��$
��Name��5   e   $	 300��6s    6:   �� $
 Name�6s   �� 5      -	   5        ProductPrice��$��Price��5   g   $ 70��6v    6;   �� $ Price�6v   �� 5       $ 30��6%    �� CellTemplate 5!      �� 5"      .R�5#   "   �� $
��💲��6B   $��30��6O   
 � Tag 6(   �� *	ProductId+6O   5$      5&      �� 5'      -
   5        ProductCategory��$��Category��5'   m   $	 150��6    6>   �� $ Category�6   5(      5*      �� 5+   
   -   5        $ 
AddItemBtn��5+      $��Add Product��6%   $
��Left=�6;   $��
334,30,0,0q�6V   $��2��6j   $	��Top=�6w   $	��147��6�   $��53��6�   ���	#FFEC7989���	#FFEC7989��6�   Z� 6�   �� 5,   
   -   5        $��
Go Back↺��5,         
classicBtn#��[  6$   $	��116��6]   $��41��6i   $��62,0,0,0q�6u   $��2��6�   $��Center=�6�   5-      5.      