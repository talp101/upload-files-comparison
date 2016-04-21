import os
from time import time

import requests


FIXTURES_PATH = "fixtures"

language_to_port = {'go':'3000', 'python':'5000','js':'7000'}
url = 'http://localhost:{port}/upload'

for language in language_to_port.keys():
    try:
        start_time = time()
        for input_file in os.listdir(FIXTURES_PATH):
            with open(r'fixtures/'+input_file, 'rb') as upload_file:
                files = {'files': upload_file}
                response = requests.post(url.format(port=language_to_port[language]), files=files)
        print language, response, time() - start_time
    except:
        continue
