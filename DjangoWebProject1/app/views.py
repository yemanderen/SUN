"""
Definition of views.
"""

from datetime import datetime
from django.shortcuts import render
from django.http import HttpRequest
from django.http import HttpResponse


def home(request):
    """Renders the home page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/index.html',
        {
            'title':'Home Page',
            'year':datetime.now().year,
        }
    )

def contact(request):
    """Renders the contact page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/contact.html',
        {
            'title':'Contact',
            'message':'Your contact page.',
            'year':datetime.now().year,
        }
    )

def about(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/about.html',
        {
            'title':'About',
            'message':'Your application description page.',
            'year':datetime.now().year,
        }
    )

def news(request):
    """Renders the news page."""
    assert isinstance(request, HttpRequest)
    views_list = ["News1","News2","News3",]
    return render(
        request,
        'app/news.html',
        {
            'title':'News',
            'message':'The latest news.',
            'year':datetime.now().year,
            'views_list':views_list
        }
    )

def search_form(request):
    return render(request, 'app/search_form.html')
 

def search(request):  
    request.encoding='utf-8'
    if 'q' in request.GET and request.GET['q']:
        message = 'Search content: ' + request.GET['q']
    else:
        message = 'Form submitted'
    return HttpResponse(message)