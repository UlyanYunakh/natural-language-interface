# -*- coding: utf-8 -*-
import nltk
import json
from pymorphy2 import MorphAnalyzer

def check_form(words_dict, form, normal):
    forms_in_dict = words_dict[normal]['word_forms']
    if form not in forms_in_dict:
        forms_in_dict.update({form: 1})
    else:
        forms_in_dict[form] += 1


def wordize(sentences):
    list_word = []
    for sent in nltk.sent_tokenize(sentences.lower()):
        for word in nltk.word_tokenize(sent):
            if word != '.' and word != ',' and word != '?' and word != '!':
                list_word.append(word)
    return list_word

# основная функция которая и нужна
def parser(text):
    json_text = ""
    # словарь
    dictionary = []
    # массив слов из текста
    words = wordize(text)
    # анализатор русских слов
    analyzer = MorphAnalyzer()

    for word in words:
        # объект типа Parse с инфой про слово, берется первый вариант разбора из всех возможных
        parse_word = analyzer.parse(word)[0]
        # само слова
        word_normal_form = parse_word.normal_form
        # словоформа
        word_form = parse_word.word.lower()
        is_wordform = True
        if word_form == word_normal_form:
            is_wordform = False

        in_dict = False
        for item in dictionary:
            if word_form == item['Word']:
                in_dict = True
                item['Frequency'] += 1
        if not in_dict:
            dictionary_item = ({'Word': word_form, 'IsWordform': is_wordform, 'Frequency': 1})
            dictionary.append(dictionary_item)
    json_text = dict_in_json(dictionary)
    print(json_text)
    return json_text

def dict_in_json(dictionary):
    string = "["
    for item in dictionary:
        string += json.dumps(item, ensure_ascii = False)
        string += ","
    string = string[0:-1]
    string += "]"
    return string


data = parser("мама папа я семья ехала на дачу даче умерли")
print(data)