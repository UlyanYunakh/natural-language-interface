# -*- coding: utf-8 -*-
from pymorphy2 import MorphAnalyzer
import nltk
import json


def wordize(sentences):
    listWord = []
    for sent in nltk.sent_tokenize(sentences.lower()):
        for word in nltk.word_tokenize(sent):
            if word != '.' and word != ',' and word != '?' and word != '!' and word != '-':
                listWord.append(word)
    return listWord


def dictInJson(dictionary):
    string = "["
    for item in dictionary:
        string += json.dumps(item, ensure_ascii=False)
        string += ","
    string = string[0:-1]
    string += "]"
    return string


# основная функция
def parseText(text):
    json = ""
    # словарь
    dictionary = []
    # массив слов из текста
    words = wordize(text)
    # анализатор русских слов
    analyzer = MorphAnalyzer()

    for word in words:
        # объект типа Parse с инфой про слово, берется первый вариант разбора из всех возможных
        parseWord = analyzer.parse(word)[0]
        # само слова
        wordNormalForm = parseWord.normal_form
        # словоформа
        wordForm = parseWord.word.lower()

        is_wordform = True
        if wordForm == wordNormalForm:
            is_wordform = False

        in_dict = False
        for item in dictionary:
            if wordForm == item['Word']:
                in_dict = True
                item['Frequency'] += 1
        if not in_dict:
            dictionaryItem = (
                {'Word': wordForm, 'IsWordform': is_wordform, 'Frequency': 1})
            dictionary.append(dictionaryItem)

    json = dictInJson(dictionary)
    return json
