   M S B A M L   `   `   `  ����L  HVNPTLabelPrinting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:VNPTLabelPrinting.uCtrlVNPTLabelPrinting.uCtrl  5      &     VNPTLabelPrinting.uCtrl.ucRunAll   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:VNPTLabelPrinting.uCtrl     5      
�������  � 	Resources  5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName��%>  '��       �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $��Border��6&   $��0,0,-4,0q�64   $��1,1,1,1q�6F   $ 2,12,0,0��6`   ._�5   "   �� $ ContentSite��63   $��Center=�6H   $��Center=�6c   $��Header��6�   $��	12,2,12,2q�6�   ��. 6�   5      5       5      P� 5       �@��
IsSelected	 . . �5   "   ��  �@K�ZIndex$	 100��6A   �� 5   "   $
 Border��6)    � �� �#43d8546S   �� 5   "   $
 Border��6)    � ��$ 1,1,1,0q�6X   5      P� 5       �@��
IsSelected	 .  . �5   "   �� $
 Border��6)    � �� �	#FFEEEEEE6S   5      P� 5       �@J�IsMouseOver	 . . �5    "   �� $
 Border��6)    � ��
 � ���6S   5!      5#      5%      5&      5(      .��5*      �  � RowDefinitions 5+   
   �� 5,      $��* �6   �� 5-      $	��150 �6   .+�50   
   � .+�51      �� -   5         ��Name$ inputBarcodeViewer��51   1   $	��Top=�6   61   �� 52      -   5         ��Name$ tabResultViewer��52       �� TabStripPlacement$ Bottom=�62     53      ��%V   '��       �� 54      " ��6   .�55      ��  � ��$ Center=�6@   56      57      .@�59      �� ��5:      �� 5;      $��Item Result��6    .��5>      &   System.Windows.Controls.DataGrid  -   5          Name$ 	dg_result��5>   0   $��0,0,0,0q�6      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserReorderColumns .  "   VerticalScrollBarVisibility$
 Auto=�5@   �   $   HorizontalScrollBarVisibility$
 Auto=�6�      GridLinesVisibility$
 None=�5A   /      HeadersVisibility$ Column=�5B   /   $��1,1,1,1q�5C   /      SelectionUnit$ FullRow=�5D   /      SelectionMode$ Extended=�6G   ��5?   /   ��    	CellStyle 5G      �� 5H      *  $System.Windows.Controls.DataGridCell"  6$   .�5I   "   ��  � ��$ 0,0,1,1q�6D   �� 5J   "    � �� �	#FFB3B3B36@   �� 5K   "    � ��$ 13(�6=   �� 5L   "    � ��$ Normal"�6?   �� 5M   "    � ��$ 25��6;   �� 5N   "    � ��$ Center=�6M   5O         ColumnHeaderStyle 5R      �� 5S      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6$   .�5T   "   ��  � ��$ 13(�6=   �� 5U   "    � ��$ 40��6;   �� 5V   "    � ��$
 Bold"�6?   �� 5W   "    � �� �	#FFB3B3B36@   �� 5X   "    � ��$ 0,0,0,1q�6D   �� 5Y   "    � ��$ 3,0,0,0q�6<   5Z         Columns 5]      0  *System.Windows.Controls.DataGridTextColumn  5^      ,  &System.Windows.Controls.DataGridColumn    Header$  Hạng mục test��61   !   
IsReadOnly! . "   Width5  /System.Windows.Controls.DataGridLengthConverter$" 0.75* 6Z   1  +System.Windows.Controls.DataGridBoundColumn#   Binding# 6h   �� *
itemName+$ �� UpdateSourceTrigger$$ PropertyChanged=�% �� Mode$% TwoWay=�4  .System.Windows.Controls.DataGridComboBoxColumn  5_      -   5        cbb_ischeck��$  Thực hiện��5_   J   $
" 0.3* 6]   &   SelectedItemBinding& 6j   �� *	isCheck+$$ PropertyChanged=�$% TwoWay=�  5`      $  Tiêu chuẩn��61   ! . $" * 6W   # 6a   �� *	itemInput+$$ PropertyChanged=�$% TwoWay=�  5a      $  Thực tế��61   ! . $
" 0.5* 6T   # 6a   �� *
itemOutput+$$ PropertyChanged=�$% TwoWay=�  5b      $  Kết quả��61   ! . $
" 0.3* 6T   # 6a   �� *
itemResult+$$ PropertyChanged=�$% TwoWay=�'   	CellStyle' 5c   "   �� 5d   &   "  6,   .�5e   *   ��  � ��$ 0,0,1,1q�6L   �� 5f   *    � �� �	#FFB3B3B36H   �� 5g   *    � ��$ 15(�6E   �� 5h   *    � ��$ SemiBold"�6G   �� 5i   *    � ��$ 25��6C   �� 5j   *    � ��$ Center=�6U   �� 5k   *    � ��
 �   �6G   ( �� Triggers( 5l   *   �� 5m   .   ) �� Binding) 6:   �� * �� Path$* 
itemResult�+ �� Value+ 
Waiting...6^   .a�5n   2   ��  � ��
 � ���6O   5o   /   �� 5p   .   ) 6:   �� $* 
itemResult�
+ Passed6^   .a�5q   2   ��  � ��
 � � �6O   5r   /   �� 5s   .   ) 6:   �� $* 
itemResult�
+ Failed6^   .a�5t   2   ��  � ��
 �  ��6O   5u   /   5w   '   5y      ,   RowStyle, 5}      �� 5~      )	  #System.Windows.Controls.DataGridRow" 	 6$   .�5   "   ��  � ��
 �   �6?   ( 5�   "   �� 5�   &   ) 62   �� *
itemResult+
+ Failed6Q   .a�5�   *   ��  � ��
 �  ��6G   5�   '   �� 5�   &   ) 62   �� *	isCheck++ Yes6N   .a�5�   *   ��  � ��
 �����6G   5�   '   �� 5�   &   ) 62   �� *	isCheck++ No6N   .a�5�   *   ��  � �� �#c7c7c76G   5�   '   5�      5�      5�      �� 5�      ��5�      �� 5�      $��
Log system��6    .��5�      �� -   5        - ��Name$- 
scr_system��5�   #   .��5�      �� . �� 
IsReadOnly. . $��0,0,1,2q�68   $��5,5,0,0q�6I   $��Stretch=�6[   $��Stretch=�6w   . �5�      /� .T�5�   "   J� .*�6,   .*�5�   &   �� �6*   �� $* 	logSystem�$$ PropertyChanged=�5�   #   5�      5�      5�      5�      5�      5�      �� 5�   
   $��2��6   .+�5�      �� $��Center=�6   $��0,5,0,0q�61   $��13(�6B   $��Total result:��6P   �� 5�      $��Center=�6   $��	0,-15,0,0q�61   $��50(�6D   $��SemiBold"�6R   ��6h   �� $* totalResult�$$ PropertyChanged=���5�      �� 5�      " ��6   .�5�      ��  � ��
 �   �67   ( 5�      �� 5�      ) 6*   �� / �� RelativeSource/ �� 0 �� Mode$
0 Self=�$* Content�+ 
Waiting...6v   .a�5�   "   ��  � ��
 � ���6?   5�      �� 5�      ) 6*   �� / �� $
0 Self=�$* Content�
+ Passed6v   .a�5�   "   ��  � ��
 � � �6?   5�      �� 5�      ) 6*   �� / �� $
0 Self=�$* Content�
+ Failed6v   .a�5�   "   ��  � ��
 �  ��6?   5�      5�      5�      �� 5�      $��	0,-10,0,0q�6   $��15(�6+   $��SemiBold"�69   $��Center=�6O   $��Center=�6j   $
��Wrap=�6�   ��6�   �� $* inputBarcode�$$ PropertyChanged=�5�      5�      5�      