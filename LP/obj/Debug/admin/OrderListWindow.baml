   M S B A M L   `   `   `  ����=  9PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLPL  5   	       PL.OrderListWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:PL     5   	     � Title$  OrderListWindow��5   	   $	��450��6!   $	��800��6.   .��5	      � .+�5
   
   �� -   5         ��Name$ orderListLbl��5
      $��orders list: ��6&   $
��Left=�6>   $��	25,21,0,0q�6Y   $	��Top=�6l   $	��128��6�   ���	#FFEC7989$
��Bold"�6�   $��22(�6�   �� 5   
   -   5         ��Name$ OrderListView��5      $��18(�6�   $��25,75,46,113q�6�    �� View 5      �� 5       �� AllowsColumnReorder$
 true��6   .P�5      �� -   5        OrderId��$��ID��5   _    �� Width$ 80��6k    �� DisplayMemberBinding 66   ��  �� Path$ ID�6k   �� 5      -   5        CustomerName��$
��Name��5   n   $	 230��6|    6;   �� $ CustomerName�6|   �� 5      -   5        ProductPrice��$��Total Price��5   l   $	 100��6�    6;   �� $ 
TotalPrice�6�   �� 5      $ 30��6%    �� CellTemplate 5      �� 5      .R�5   "   �� $
��💲��6B   $��30��6O   
	 � Tag	 6(   �� *	ProductId+6O   5      5      �� 5      -   5        ProductCategory��$��Status��5   k   $	 120��6{    6>   �� $ Status�6{   �� 5      -   5        AmountOfItems��$��Amount of items��5   p   $	 140��6�    6<   �� $ AmountOfItems�6�   5      5      �� 5   
   -   5        $��
Go Back↺��5         
classicBtn#��[  6$   $��
36,357,0,0q�6]   $	��106��6q   $��40��6}   5!      5"      