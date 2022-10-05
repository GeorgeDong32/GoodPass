/* GPHES.cpp version 2.6.0     */
#include <cstring>
#include <fstream>
#include "GPHES.h"
/*≤‚ ‘ π”√
#include <iostream>
using std::cout; using std::endl;*/
/***************************************************************
*                                                              *
* GPEHS.h -- Provides GoodPass Hash Encryption System          *
* GPHES is a hash-based encrypton system developed by GoodPass *
* Copyright(c) GeorgeDong32(Github).All rights reserved.       *
*                                                              *
***************************************************************/
/*******************************************************************
*                                                                  *
* The SHA256 class and its related fuctions are provides by jluuM2 *
* Copyright(c) jluuM2(Github).All rights reserved.                 *
* Follows the license below                                        *
* https://github.com/jluuM2/sha256/blob/master/sha256/licence.txt  *
*                                                                  *
*******************************************************************/
const unsigned int SHA256::sha256_k[64] = //UL = uint32
{ 0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5,
0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3,
0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc,
0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7,
0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13,
0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3,
0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5,
0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208,
0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2 };

void SHA256::transform(const unsigned char* message, unsigned int block_nb)
{
	uint32 w[64];
	uint32 wv[8];
	uint32 t1, t2;
	const unsigned char* sub_block;
	int i;
	int j;
	for (i = 0; i < (int)block_nb; i++) {
		sub_block = message + (i << 6);
		for (j = 0; j < 16; j++) {
			SHA2_PACK32(&sub_block[j << 2], &w[j]);
		}
		for (j = 16; j < 64; j++) {
			w[j] = SHA256_F4(w[j - 2]) + w[j - 7] + SHA256_F3(w[j - 15]) + w[j - 16];
		}
		for (j = 0; j < 8; j++) {
			wv[j] = m_h[j];
		}
		for (j = 0; j < 64; j++) {
			t1 = wv[7] + SHA256_F2(wv[4]) + SHA2_CH(wv[4], wv[5], wv[6])
				+ sha256_k[j] + w[j];
			t2 = SHA256_F1(wv[0]) + SHA2_MAJ(wv[0], wv[1], wv[2]);
			wv[7] = wv[6];
			wv[6] = wv[5];
			wv[5] = wv[4];
			wv[4] = wv[3] + t1;
			wv[3] = wv[2];
			wv[2] = wv[1];
			wv[1] = wv[0];
			wv[0] = t1 + t2;
		}
		for (j = 0; j < 8; j++) {
			m_h[j] += wv[j];
		}
	}
}

void SHA256::init()
{
	m_h[0] = 0x6a09e667;
	m_h[1] = 0xbb67ae85;
	m_h[2] = 0x3c6ef372;
	m_h[3] = 0xa54ff53a;
	m_h[4] = 0x510e527f;
	m_h[5] = 0x9b05688c;
	m_h[6] = 0x1f83d9ab;
	m_h[7] = 0x5be0cd19;
	m_len = 0;
	m_tot_len = 0;
}

void SHA256::update(const unsigned char* message, unsigned int len)
{
	unsigned int block_nb;
	unsigned int new_len, rem_len, tmp_len;
	const unsigned char* shifted_message;
	tmp_len = SHA224_256_BLOCK_SIZE - m_len;
	rem_len = len < tmp_len ? len : tmp_len;
	memcpy(&m_block[m_len], message, rem_len);
	if (m_len + len < SHA224_256_BLOCK_SIZE) {
		m_len += len;
		return;
	}
	new_len = len - rem_len;
	block_nb = new_len / SHA224_256_BLOCK_SIZE;
	shifted_message = message + rem_len;
	transform(m_block, 1);
	transform(shifted_message, block_nb);
	rem_len = new_len % SHA224_256_BLOCK_SIZE;
	memcpy(m_block, &shifted_message[block_nb << 6], rem_len);
	m_len = rem_len;
	m_tot_len += (block_nb + 1) << 6;
}

void SHA256::final(unsigned char* digest)
{
	unsigned int block_nb;
	unsigned int pm_len;
	unsigned int len_b;
	int i;
	block_nb = (1 + ((SHA224_256_BLOCK_SIZE - 9)
		< (m_len % SHA224_256_BLOCK_SIZE)));
	len_b = (m_tot_len + m_len) << 3;
	pm_len = block_nb << 6;
	memset(m_block + m_len, 0, pm_len - m_len);
	m_block[m_len] = 0x80;
	SHA2_UNPACK32(len_b, m_block + pm_len - 4);
	transform(m_block, block_nb);
	for (i = 0; i < 8; i++) {
		SHA2_UNPACK32(m_h[i], &digest[i << 2]);
	}
}

std::string sha256(std::string input)
{
	unsigned char digest[SHA256::DIGEST_SIZE];
	memset(digest, 0, SHA256::DIGEST_SIZE);

	SHA256 ctx = SHA256();
	ctx.init();
	ctx.update((unsigned char*)input.c_str(), input.length());
	ctx.final(digest);

	char buf[2 * SHA256::DIGEST_SIZE + 1];
	buf[2 * SHA256::DIGEST_SIZE] = 0;
	for (int i = 0; i < SHA256::DIGEST_SIZE; i++)
		sprintf(buf + i * 2, "%02x", digest[i]);
	return std::string(buf);
}

string gphes(string mainkey)
{
	GPHES o(mainkey);
	string stemp;
	stemp = o.getSalt();
	o.Mix();
	string sstr = o.getSaltedstr();
	return sha256(sstr);
}

GPHES::GPHES() :
	mainkey_s("mainkey"),
	salt_s("defualt_salt"),
	salted_mainkey("salted_mainkey") {}

GPHES::GPHES(string mainkey) :
	mainkey_s(mainkey),
	salt_s("defualt_salt"),
	salted_mainkey("salted_mainkey") {}

string GPHES::getSalt()
{
	string resalt = "12345678";
	int saltbase[8] = { 0, };
	int* mkbase = new int[mainkey_s.length()];
	int mkl = mainkey_s.length();
	for (int i = 0; i < mkl; i++)
	{
		if (mainkey_s[i] <= 'z' && mainkey_s[i] >= 'a')
			mkbase[i] = mainkey_s[i] - 'a';
		else if (mainkey_s[i] <= 'Z' && mainkey_s[i] >= 'A')
			mkbase[i] = mainkey_s[i] - 'A';
		else if (mainkey_s[i] <= '9' && mainkey_s[i] >= '0')
			mkbase[i] = mainkey_s[i] - '0';
		else
			mkbase[i] = 0;
	}
	int addcon = mkl / 8; int j = 0; int mkbc = 0;
	for (int i = 0; i < 8; i++)
	{
		j = 0;
		for (j; j < addcon; j++)
		{
			saltbase[i] += mkbase[mkbc];
			mkbc++;
		}
		if (saltbase[i] <= 32)
		{
			saltbase[i] += 32;
		}
		while (saltbase[i] >= 127)
		{
			saltbase[i] -= 32;
		}
	}
	for (int i = 0; i < 8; i++)
	{
		resalt[i] = saltbase[i];
	}
	salt_s = resalt;
	//cout << "resalt is" << resalt << endl;
	return resalt;
}

string GPHES::Mix()
{
	string remix; string s1, s2;
	s1 = "0000"; s2 = "0000";
	for (int i = 0; i < 4; i++)
		s1[i] = salt_s[i];
	for (int i = 0; i < 4; i++)
		s2[i] = salt_s[i + 4];
	remix = s1 + mainkey_s + s2;
	salted_mainkey = remix;
	//cout << "The salted_mainkey is:" << endl << salted_mainkey << endl;
	return remix;
}

string GPHES::getSaltedstr()
{
	return salted_mainkey;
}
