import jsonpath_ng
import requests
import json
from Utils.TextUtils import clean_txt, generate_title
from Utils.Markov import make_markov_model
import random

response = requests.get("https://localhost:7244/api/trending/US/false", verify=False)
response2 = requests.get("https://localhost:7244/api/trending/GB/false", verify=False)

jsonArray = json.loads(response.content)
jsonArray2 = json.loads(response2.content)
jsonArray = jsonArray + jsonArray2
jsonpath_expr = jsonpath_ng.parse('*.title')

titles = [value['title'] for value in jsonArray]

cleaned_stories = clean_txt(titles)

markov_model = make_markov_model(cleaned_stories, 1)
newtitles = []
for _ in range(1000):
    title = generate_title(markov_model, start=random.choice(cleaned_stories), limit=random.randint(3,11))
    newtitles.append(title)
    print(title)
