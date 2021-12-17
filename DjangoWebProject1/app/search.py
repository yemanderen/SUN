from django.http import HttpResponse
from django.shortcuts import render

def search_form(request):
    return render(request, 'app/search_form.html')
 

def search(request):  
    request.encoding='utf-8'
    if 'q' in request.GET and request.GET['q']:
        message = 'Search content: ' + request.GET['q']
    else:
        message = 'Form submitted'
    return HttpResponse(message)