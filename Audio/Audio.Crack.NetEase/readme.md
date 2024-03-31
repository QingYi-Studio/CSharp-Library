# Audio.Crack.NetEase

A GitHub based on many open source projects and you give the solution to produce a simple NetEase cloud music download down NCM files into MP3 files.

Of course, the premise is that there must be VIP, not cracked VIP so that you can download without VIP.

I'll never support old .NET versions.

## Usage

```c#
string inputFilePath = "input.ncm";
string outputFilePath = "output.mp3";
NcmDecryptor.Decrypt(inputFilePath, outputFilePath);
```

## NCM file structure

| ��Ϣ| ��С| ˵��|
|-------|------|------|
|Magic Header|10 bytes|�ļ�ͷ|
|Key Length|4 bytes|AES128���ܺ��RC4��Կ���ȣ��ֽ��ǰ�С������|
|Key Data|Key Length|��AES128���ܺ��RC4��Կ��<br/>1. �Ȱ��ֽڶ�0x64�������<br/>2. AES����,ȥ����䲿�֡�<br/>3. ȥ����ǰ��'neteasecloudmusic'17���ֽڣ��õ�RC4��Կ��|
|Music Info Length|4 bytes|���������Ϣ�ĳ��ȣ�С������|
|Music Info Data|Music Info Length|Json��ʽ������Ϣ���ݡ�</br>1. ���ֽڶ�0x63�������<br/>2. ȥ����ǰ��22���ֽڡ�<br/>3. Base64���н��롣<br/>4. AES���ܡ�<br/>6. ȥ��ǰ��6���ֽڵõ�Json���ݡ�|
|CRC|4 bytes|����|
|Gap|5 bytes|����|
|Image Size|4 bytes|ͼƬ�Ĵ�С|
|Image|Image Size|ͼƬ����|
|Music Data| - |1. RC4-KSA����S�С�<br/>2. ��S�н��ܣ��Զ���Ľ��ܷ���)������RC4-PRGA���ܡ�|

����AES��Ӧ��Կ
`unsigned char meta_key[] = { 0x23,0x31,0x34,0x6C,0x6A,0x6B,0x5F,0x21,0x5C,0x5D,0x26,0x30,0x55,0x3C,0x27,0x28 };`
`unsigned char core_key[] = { 0x68,0x7A,0x48,0x52,0x41,0x6D,0x73,0x6F,0x35,0x6B,0x49,0x6E,0x62,0x61,0x78,0x57 };`
