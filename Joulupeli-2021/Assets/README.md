# Important info about implementation

## WebGLInput package

On WebGL player the package requires `position: relative` for the parent
element of the Unity game canvas, so that absolutely positioned
elements inside this container are positioned relative to the
container and not to the document. This is needed for the WebGLInput
addon to work. It fixes input fields for touch screens on WebGL.
