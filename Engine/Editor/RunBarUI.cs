using System.Text.RegularExpressions;
using System.Reflection;
using CliqueEngine.UI;
using CliqueEngine.Nodes;
using CliqueEngine.Extensions;

namespace CliqueEngine.Editor;

class RunBarUI : UIElement
{
	HorizontalLayout horizontalLayout;
	Button playPauseBtn;

	public RunBarUI(UIRoot root) : base()
	{
		parent = root;
		anchor = new Vector2f(.5f, 0);
		horizontalLayout = new HorizontalLayout() { parent = this };
		playPauseBtn = new Button("Pause") { parent = horizontalLayout };
		playPauseBtn.onClick += OnPlayPauseClick;

		Button toggleEditor = new Button("Editor") { parent = horizontalLayout };
		toggleEditor.onClick += ToggleEditor;

		Engine.instance.onWindowResized += (int widht, int height) => localPosition = new Vector2f(widht / 2, 0);
	}

	void OnPlayPauseClick()
	{
		if (Engine.instance.IsRunnig)
		{
			Engine.instance.Pause();
			playPauseBtn.text = "Play";
		}
		else
		{
			Engine.instance.Resume();
			playPauseBtn.text = "Pause";
		}
	}

	void ToggleEditor()
	{
		for (int i = 0; i < parent.children.Count; i++)
		{
			if (parent.children[i] == this) continue;
			parent.children[i].enabled = !parent.children[i].enabled;
		}
	}

}