   M S B A M L   `   `   `  ����L  HVNPTLabelPrinting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:VNPTLabelPrintingVNPTLabelPrinting  5   	   U/clr-namespace:VNPTLabelPrinting.Function.Custom!VNPTLabelPrinting.Function.Custom  5   	   #    VNPTLabelPrinting.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:VNPTLabelPrinting     5   	   Bcustom/clr-namespace:VNPTLabelPrinting.Function.Custom     5   	     � Title$F  @Build Time: 01/11/2021 09:30 - Copyright of VNPT Technology 2021��5	   	   $	��500��6R   $	��700��6_    � WindowStartupLocation$ CenterScreen=�6k    � 
ResizeMode$ NoResize=�6�   ���#ffffff � 	Resources 5      0   *VNPTLabelPrinting.StringToBooleanConverter   StringToBooleanConverter��%   &	          5   
   6)   5      .��5      � $��5,5,5,5q�6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   �� $��15(�6   $��SemiBold"�6   ���#000000$��
0,0,0,0.75q�6J   $��Center=�5      $+��%Vui lòng chọn loại tem cần in:��5      �� 5   
   $
��Auto=�6   $
��Auto=�6:   $��1��6_   $��20,10,10,10q�6l   $��0,0,0,0q�6�   .��5      R� -   5        $��trvFamilies��5      $��0,0,0,0q�6*    5       �� Color �� DataType/   )VNPTLabelPrinting.Function.Custom.Project �� ItemsSource �� Orientation/   )VNPTLabelPrinting.Function.Custom.Product5   /VNPTLabelPrinting.Function.Custom.ProductMember	 d� 	IsChecked
 �� Path �� UpdateSourceTrigger �� 
TargetType �� Property �� Value �� Triggers �� Binding �� Value��%O  (��       5   &   * SystemColors.HighlightBrushKey+)(�� !     5    /   *, +)(�� �     5&   /   *, +)(�� �    5/   #   *, +)�� 5      $ #e0f5ab��6X   �� 5       "  6/    6R   �� *
Products+.R�5!      �� $ 
Horizontal=�6%   $��0,2,0,0q�6>   .+�5"      �� 
���   �$��17(�6;   $
��Bold"�6I   ��6[   �� *Name+5#      5$      �� 5&      "  6/    6R   �� *	Members+.R�5'      �� $ 
Horizontal=�6%   $��0,2,0,0q�6>   .+�5(      �� 
���   �$��15(�6;   $��SemiBold"�6I   ��6_   �� *Name+�� 5)      $��13(�6(   $�� [��66   
����  �6@   �� 5*      $��13(�6(   
����  ���66   �� *Members.Count+6U   �� 5+      $��13(�6(   $��]��66   
����  �6?   5,      5-      �� 5/      "  6#   .R�50      �� $ 
Horizontal=�6%   .+�51      �� -   5        $��0,0,5,0q�51   �   ��6'   �� *Name+	 6�   �� $
 	isChecked�$ PropertyChanged=���52   "   �� 53   &   " ��6,   .�54   *   ��  � ��$ 13(�6E   �� 55   *    � ��$ Normal"�6G   �� 56   *    � ��
 �   �6G   �� 57   *    � ��$ 0,0,0,0q�6D    58   *   �� 59   .    6:   �� $
 	isChecked� True6]   .a�5:   2   ��  � ��$ SemiBold"�6O   �� 5;   2    � ��
 �  ��6O   5<   /   5>   '   5@      5A      5B      5C      5D      5E      �� 5G   
   $��2��6   .+�5H      �� ���#0000006   � 5I      $��0,3,0,0q�6   .+�5J      �� -   5         ��Name$ label_selected��5J   W   $
��Left=�6   $��15(�63   $��SemiBold"�6A   $�� ��6o   �� 5K      -   5        $��Right=�5K   .   $
��Next��6J   $��25(�6Y   $��40��6h   $	��120��6t   {�6�   ��  �� ElementName$ label_selected��$
 Content� �� 	Converter# [  $ PropertyChanged=�5L      5O      5S      5^      