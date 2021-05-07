# -*- coding: utf-8 -*-
import spacy
from spacy.lang.ru.examples import sentences
from nltk import ParentedTree
import nltk
import json

nlp = spacy.load("ru_core_news_md")

def tok_format(tok):
    return "_".join([tok.orth_, tok.tag_])

def to_nltk_tree(node):
    if node.n_lefts + node.n_rights > 0:
        return ParentedTree(tok_format(node), [to_nltk_tree(child) for child in node.children])
    else:
        return tok_format(node) 

def get_model(text):
    sents = []
    trees = []
    doc = nlp(text)
    for sent in doc.sents:
        sents.append(str(sent))
        trees.append(to_nltk_tree(sent.root))
    trees_str = []
    for tree in trees:
        trees_str.append(str(tree))
    model = {
        'sents': sents,
        'trees': trees_str
    }
    return model

def model_to_json(model):
    sents_json = json.dumps(model.get('sents'), ensure_ascii=False)
    trees_json = json.dumps(model.get('trees'), ensure_ascii=False)
    model_json = "{\"sents\":" + sents_json + ",\"trees\":" + trees_json + "}"
    return model_json


def parseText(text):
    model = get_model(text)
    model_json = model_to_json(model)
    return model_json
