   M S B A M L   `   `   `  ����  PL    PL.OrderTrackingWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  OrderTrackingWindow��5   	   $	��450��6%   $	��800��62   .��5      � .+�5	   
   �� -   5         ��Name$ IdOrderLabel��5	         
classicLbl#��[  6&   $��	85,33,0,0q�6J   $��Order Number��6]   $	��180��6t   $��31��6�   �� 5
   
   -   5        $ StatusLabel��5
      #��[  6%   $��
387,33,0,0q�6I   $��Status��6]   $��74��6n   $��31��6y   �� 5   
   -   5         ��Name$ 
StatusText��5        classicText#��[ 6&   $
��Left=�6K   $��
484,33,0,0q�6f    �� TextWrapping$
 Wrap=�6z   $	��Top=�6�   $	��120��6�   $��31��6�   ��6�   �� *Status+6�   �� 5   
   #��[  6   $��
Track List��64   $��
97,122,0,0q�6I   $��91��6]   $��36��6h   �� 5   
   -   5         ��Name$ 	TrackList��5      $��220,122,235,162q�6s   ��6Q   �� *	TrackList+ �� View 5      �� 5      .P�5      �� -   5        DateCol��$
��Date��5   ]    �� Width$	 167��6k    �� DisplayMemberBinding 66   �� *Item1+6k   �� 5      -   5        	StatusCol��$��Status��5   _   $	 165��6o    68   �� *Item2+6o   5      5      �� 5   
   -   5          
classicBtn#��[ 5      $��Order Details��65   $��306,337,0,0q�6h   $	��174��6}   $��37��6�   5      5      