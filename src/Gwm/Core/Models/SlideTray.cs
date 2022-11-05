using System.Collections;

namespace gwm.Core.Models;

public class SlideTray : IEnumerable<ISlide>
{
    private readonly LinkedList<ISlide> _slides;
    private readonly ISlide _blankSlide;
    private LinkedListNode<ISlide> _currSlideNode;

    public SlideTray(ISlide blankSlide)
    {
        _slides = new LinkedList<ISlide>();
        _blankSlide = blankSlide;
        _currSlideNode = _slides.AddFirst(blankSlide);
    }

    public ISlide CurrentSlide => _currSlideNode.Value;

    public void Add(ISlide slide)
    {
        _slides.AddLast(slide);
    }

    public void Remove(ISlide slide)
    {
        if (IsBlankSlide(slide))
            return;
        _slides.Remove(slide);
    }

    public bool Contains(ISlide slide)
    {
        return _slides.Contains(slide);
    }
    
    public void CycleDown()
    {
        var prevSlideNode = _currSlideNode.Previous ?? _slides.Last;
        Show(prevSlideNode!);
    }
    
    public void CycleUp()
    {
        var nextSlideNode = _currSlideNode.Next ?? _slides.First;
        Show(nextSlideNode!);
    }

    public bool IsBlankSlide(ISlide slide)
    {
        return slide == _blankSlide;
    }

    public void Show(ISlide slide)
    {
        var slideNode = _slides.Find(slide);
        Show(slideNode!);
    }

    private void Show(LinkedListNode<ISlide> slideNode)
    {
        CurrentSlide.Hide();
        _currSlideNode = slideNode;
        CurrentSlide.Show();
    }

    public IEnumerator<ISlide> GetEnumerator()
    {
        return _slides.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _slides.GetEnumerator();
    }
}