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
    # словарь
    dictionary = {}
    # массив слов из текста
    words = wordize(text)
    # анализатор русских слов
    analyzer = MorphAnalyzer()

    for word in words:
        # объект типа Parse с инфой про слово, берется первый вариант разбора из всех возможных
        parse_word = analyzer.parse(word)[0]
        # само слова
        word_normal_form = parse_word.normal_form
        # характеристики
        word_tags = parse_word.tag.cyr_repr
        # словоформа
        word_form = parse_word.word.lower()

        if word_normal_form not in dictionary:
            '''
            Структура словаря: 
            -слово:
                -количество использования слова
                формы:
                    -форма
                    -количество использования формы
            -тэг
            '''
            dictionary.update({word_normal_form: {
                'count': 1, 
                'word_forms': {word_form: 1}, 
                'tag': word_tags}
                })
        else:
            check_form(dictionary, word_form, word_normal_form)
            values = dictionary.get(word_normal_form)
            # добавление количества использования слова
            values['count'] += 1
    # возвращает отсортированный словарь, сортировка по основной форме слова
    # создает файл
    with open('dict.json', 'w') as json_file:
        # переводит в json, если не работает попробуй убрать 'ensure_ascii = False'
        json.dump(dictionary, json_file, ensure_ascii = False)
    return sorted(dictionary.items(), key=lambda word: word[0])

data = parser("мама папа я семья ехала на дачу даче умерли")
print(data)