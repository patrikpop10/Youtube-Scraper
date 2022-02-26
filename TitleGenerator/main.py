import jsonpath_ng
import requests
import json
from Utils.TextUtils import clean_txt, generate_title
from Utils.Markov import make_markov_model
from Utils.dict_utils import remove_element
import random
import pandas as pd

response = requests.get("https://localhost:7244/api/trending/US/false", verify=False)
response2 = requests.get("https://localhost:7244/api/trending/GB/false", verify=False)
response3 = requests.get("https://localhost:7244/api/trending/AU/false", verify=False)

jsonArray = json.loads(response.content)
jsonArray2 = json.loads(response2.content)
jsonArray3 = json.loads(response3.content)
jsonArray = [remove_element(val,'uploader') for val in jsonArray]
jsonArray2 = [remove_element(val,'uploader') for val in jsonArray2]
jsonArray3 = [remove_element(val,'uploader') for val in jsonArray3]

jsonArray = jsonArray + jsonArray2 + jsonArray3
df = pd.DataFrame(jsonArray)
print(df)
jsonpath_expr = jsonpath_ng.parse('*.title')

titles = [value['title'] for value in jsonArray]

cleaned_titles = clean_txt(titles)

markov_model = make_markov_model(cleaned_titles, 1)
newtitles = []
for _ in range(25):
    title = generate_title(markov_model, start=random.choice(cleaned_titles), limit=random.randint(3,11))
    newtitles.append(title)
    print(title)
