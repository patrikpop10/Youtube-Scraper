def remove_element(dictionary, element):
    r = dict(dictionary)
    del r[element]
    return r


def summarize_inner_dict(dictionary, key, key_to_use_for_summary):
    dictionary[key] = dictionary[key][key_to_use_for_summary]
    return dictionary

