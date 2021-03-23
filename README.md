# FM-SoundEffectTrackMML
[MUCOM88](https://onitama.tv/mucom88/)  のCパート(効果音モード)で、最大4トラックを鳴らす為のラッパーMMLです。  

実行ファイルのダウンロードは[こちら](https://github.com/DM-88mkII/FM-SoundEffectTrackMML/blob/main/FM-SoundEffectTrackMML/bin/Release/FM-SoundEffectTrackMML.exe)  

<br>

# 使い方
~~~
FM-SoundEffectTrackMML.exe 入力ファイル

エラーが無ければ、変換後の文字列がクリップボードにコピーされます。
~~~

<br>

# 書式
[MUCOM88](https://onitama.tv/mucom88/) に似せた書式になっています。  
~~~
;設定
#OCTAVE_UPDOWN [><|<>]
#VOLUME_UPDOWN [)(|()]

;音色定義
  @No:{
  FB
  AR1, DR1, SR1, RR1, SL1, TL1, KS1, MT1, DT1
  AR2, DR2, SR2, RR2, SL2, TL2, KS2, MT2, DT2
  AR3, DR3, SR3, RR3, SL3, TL3, KS3, MT3, DT3
  AR4, DR4, SR4, RR4, SL4, TL4, KS4, MT4, DT4, "Name"}

;マクロ定義
# *n{}

;トラック定義
1 スロット1のトラック
2 スロット2のトラック
3 スロット3のトラック
4 スロット4のトラック
~~~
変換後は、Cパートに纏められます。  

<br>

# MMLコマンド
[MUCOM88](https://onitama.tv/mucom88/) に似せたコマンドになっています。  
~~~
音符
    音階      c d e f g a b （直後に、+を付けると♯、-を付けると♭）
    休符      r

コマンド
    音階      > < K k o D
    音量      ) ( V v
    音長      C l q . & ^ %
    リピート  [ ] /
    テンポ    t T
    音色      @
    マクロ    *
    その他    ; : ! |

pコマンド
    pM 出力なし
    pL 左出力
    pR 右出力
    pC 中央出力

yコマンド
    yFB,パラメータ
    yAL,パラメータ
    yAR,パラメータ
    yDR,パラメータ
    ySR,パラメータ
    yRR,パラメータ
    ySL,パラメータ
    yTL,パラメータ
    yKS,パラメータ
    yMT,パラメータ
    yDT,パラメータ
    ySE,パラメータ
    パラメータの頭文字に $ を付けると、16進数で指定できます。
~~~

* 備考
  * 発音可能な最大トラック数は、ALのキャリア数です。
  * モジュレータに該当するトラックの機能は、変調周波数の操作となります。
  * @コマンドで音色を設定した後は、必ずキャリアに該当するトラックで音量の再設定を行ってください。
  * pコマンドは、全スロットのトラックに掛かります。
  * Lコマンドは、スロット1のトラックの指定が有効となり、他のトラックは強制的にループが適用されます。
  * tコマンドとTコマンドは、全トラックに掛かります。
  * ポルタメントとソフトウェアLFOの実装は見送りました。
    * 唯でさえ変換後のMMLが膨大な上、更に肥大化する為。

<br>

# 制限

* MUCOM88 は、純粋に状態を維持するコマンドが存在しない為、変換の際、2クロック分の前処理MMLを挿入していますので、テンポずれにご注意下さい。

<br>

# その他

変換後のMMLは、yコマンドが膨大な量となる為、driver は、次のものに設定することを推奨します。
* mucom88EM
* mucomDotNET
