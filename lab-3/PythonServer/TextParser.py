import pymorphy2
from nltk.tokenize import word_tokenize
from ruconceptnet import ConceptNet
import json


def getSentence(text):
    forbiddenSymbols = [",", ".", "!", ":", "?", "...", ";", "-", "(", ")", "\"",]
    morph = pymorphy2.MorphAnalyzer()
    parsed_words = word_tokenize(text)
    words = []
    for word in parsed_words:
        if word not in forbiddenSymbols:
            words.append(morph.parse(word)[0].normal_form)
    return words


def getModel(conceptNet, word):
    synonym = []
    relatedTo = []
    antonym = []
    hyponym = []
    hyperonym = []
    definition = []
    targets = conceptNet.get_targets(word)
    for target in targets:
        if 'Synonym' in target[1]:
            synonym.append(target[0])
        elif 'RelatedTo' in target[1]:
            relatedTo.append(target[0])
        elif 'Antonym' in target[1]:
            antonym.append(target[0])
        elif 'AKindOf' in target[1]:
            hyponym.append(target[0])
        elif 'PartOf' in target[1]:
            hyperonym.append(target[0])
        elif 'IsA' in target[1]:
            definition.append(target[0])
    model = {
        'word': word,
        'synonym': synonym,
        'relatedTo': relatedTo,
        'antonym': antonym,
        'hyponym': hyponym,
        'hyperonym': hyperonym,
        'definition': definition
    }
    return model


def wordsToJson(words):
    wordsJson = "["
    for word in words:
        wordsJson += json.dumps(word, ensure_ascii=False)
        wordsJson += ","
    wordsJson = wordsJson[0:-1]
    wordsJson += "]"
    return wordsJson


def parseText(text):
    sentence = getSentence(text)
    words = []
    conceptNet = ConceptNet()
    for word in sentence:
        words.append(getModel(conceptNet, word))
    wordsJson = wordsToJson(words)
    print(wordsJson)
    return wordsJson
