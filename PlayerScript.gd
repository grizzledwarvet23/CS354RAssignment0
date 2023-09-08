extends CharacterBody2D


@export var MAX_SPEED = 300
@export var ACCELERATION = 1500
@export var FRICTION = 1200
@export var GRAVITY = 1000
@export var JUMP_SPEED = 900

@onready var axis = Vector2.ZERO




func _physics_process(delta):
	# Apply gravity only if not on the ground
	#move_and_slide()
	move(delta)
	
func get_input_axis():
	axis.x = int(Input.is_action_pressed("move_right")) - int(Input.is_action_pressed("move_left"))
	#axis.y = int(Input.is_action_pressed("move_down")) - int(Input.is_action_pressed("move_up"))
	return axis.normalized()
	
func move(delta):
	axis = get_input_axis()
	if(axis == Vector2.ZERO):
		apply_friction(FRICTION * delta)
	else:
		apply_movement(axis * ACCELERATION * delta)
		
	if not is_on_floor():
		velocity.y += GRAVITY * delta
		
	if(Input.is_action_just_pressed("jump")):
		velocity.y -= JUMP_SPEED
	
	move_and_slide()
	
	
	
func apply_friction(amount):
	if velocity == Vector2.ZERO:
		if velocity.length() > amount: #velocity.length is magnitude of velocity.
			velocity -= velocity.normalized() * amount
		#else: #so if it is too small, just set velocity to zero
		#	velocity = Vector2.ZERO
		
func apply_movement(accel):
	velocity += accel
	velocity = velocity.limit_length(MAX_SPEED)
	
	

