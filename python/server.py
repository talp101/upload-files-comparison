import os
import json
from uuid import uuid4

from flask import Flask, request, redirect, url_for, jsonify
from werkzeug import secure_filename

UPLOAD_FOLDER = './uploads'

app = Flask(__name__)
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER

@app.route('/upload', methods=['POST'])
def upload_file():
    if request.method == 'POST':
        uploaded_file = request.files['files']
        if uploaded_file:
            file_name = str(uuid4())
            uploaded_file.save(os.path.join(app.config['UPLOAD_FOLDER'], file_name))
            return jsonify({'file_name':file_name })

if __name__ == "__main__":
    app.run()
